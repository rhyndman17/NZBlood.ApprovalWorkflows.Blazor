namespace NZBlood.ApprovalWorkflows.Blazor.Models;

public sealed record AttachmentFile(string FileName, byte[] Content, string ContentType);
