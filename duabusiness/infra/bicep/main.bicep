@description('Deployment environment name: dev, stage, or prod.')
param environmentName string

@description('Azure location for all resources.')
param location string = resourceGroup().location

@description('Base application name used to compose resource names.')
param applicationName string = 'dua-streamliner'

var suffix = '${applicationName}-${environmentName}'

resource servicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: '${suffix}-plan'
  location: location
  sku: {
    name: 'P1v3'
    tier: 'PremiumV3'
    size: 'P1v3'
    capacity: 2
  }
  kind: 'linux'
}

resource insights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${suffix}-appi'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Bluefield'
  }
}

resource storage 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: toLower(replace('${applicationName}${environmentName}stg', '-', ''))
  location: location
  sku: {
    name: 'Standard_RAGRS'
  }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
    supportsHttpsTrafficOnly: true
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: '${suffix}-kv'
  location: location
  properties: {
    tenantId: subscription().tenantId
    sku: {
      family: 'A'
      name: 'standard'
    }
    enableRbacAuthorization: true
    publicNetworkAccess: 'Enabled'
    enabledForDeployment: true
  }
}

resource sqlServer 'Microsoft.Sql/servers@2023-08-01-preview' = {
  name: '${suffix}-sql'
  location: location
  properties: {
    administratorLogin: 'sqladminuser'
    administratorLoginPassword: 'ReplaceDuringProvisioning!'
    publicNetworkAccess: 'Disabled'
    version: '12.0'
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  name: '${sqlServer.name}/duastreamliner'
  location: location
  sku: {
    name: 'GP_S_Gen5_2'
    tier: 'GeneralPurpose'
  }
  properties: {
    zoneRedundant: true
    readScale: 'Disabled'
  }
}

resource notificationHubNamespace 'Microsoft.NotificationHubs/namespaces@2023-09-01' = {
  name: '${suffix}-nhns'
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}

resource notificationHub 'Microsoft.NotificationHubs/namespaces/notificationHubs@2023-09-01' = {
  name: '${notificationHubNamespace.name}/dua-status'
  location: location
}

resource apiManagement 'Microsoft.ApiManagement/service@2023-05-01-preview' = {
  name: '${suffix}-apim'
  location: location
  sku: {
    name: 'Developer'
    capacity: 1
  }
  properties: {
    publisherEmail: 'platform@duastreamliner.example.com'
    publisherName: 'Dua Streamliner'
  }
}

resource apiApp 'Microsoft.Web/sites@2023-12-01' = {
  name: '${suffix}-api'
  location: location
  kind: 'app,linux'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: servicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      minTlsVersion: '1.2'
      appSettings: [
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: insights.properties.ConnectionString
        }
        {
          name: 'KeyVault__VaultUri'
          value: keyVault.properties.vaultUri
        }
      ]
    }
  }
}

resource workerApp 'Microsoft.Web/sites@2023-12-01' = {
  name: '${suffix}-worker'
  location: location
  kind: 'app,linux'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: servicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      minTlsVersion: '1.2'
      alwaysOn: true
    }
  }
}

output apiAppName string = apiApp.name
output workerAppName string = workerApp.name
output apimName string = apiManagement.name
