namespace DuaBusiness.Domain.Enums;

public enum ProcessingStage
{
    Intake = 0,
    Storage = 1,
    Ocr = 2,
    Parsing = 3,
    SemanticInterpretation = 4,
    Validation = 5,
    DuaGeneration = 6,
    Notification = 7
}
