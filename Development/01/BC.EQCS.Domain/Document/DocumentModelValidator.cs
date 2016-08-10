using BC.EQCS.Models;
using FluentValidation;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace BC.EQCS.Domain.Document
{
    public class DocumentModelValidator : ModelValidator<DocumentModel>
    {
        public DocumentModelValidator()
        {
            RuleFor(m => m.ContentName).NotNull().WithMessage("File name is required");

            RuleFor(m => m.ContentName).Length(1, 255)
                .WithMessage("File Name Too Long");

            Custom(m =>
            {
                if (m.ContentType == null)
                {
                    return new ValidationFailure("ContentType", "Invalid format. Following are the supported file formats (pdf, doc, docx or jpg)");
                }

                switch (m.ContentType)
                {
                    case "pdf":
                    case "doc":
                    case "docx":
                    case "jpg":
                        return null;
                    default:
                        return new ValidationFailure("ContentType", "Invalid format. Following are the supported file formats (pdf, doc, docx or jpg)");
                }
            });

            Custom(m =>
            {
                if (m.Content != null && m.Content.Length <= 0)
                {
                    return new ValidationFailure("Content", "Empty file cannot be uploaded.");
                }

                if (m.Content != null && m.Content.Length / (1024 * 1024) > 10)
                {
                    return new ValidationFailure("Content", "File size should not exceed 10mb.");
                }

                return null;
            });
        }
    }
}