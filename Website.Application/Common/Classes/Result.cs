using FluentValidation.Results;

namespace Website.Application.Common.Classes
{
    public class Result
    {
        public bool Succeeded { get; }
        public object? ObjContent { get; }
        public double? DblContent { get; }
        public string? StrContent { get; }
        public int? IntContent { get; }
        public List<ValidationFailure> Failures { get; } = new List<ValidationFailure>();


        private Result()
        {
            Succeeded = true;
        }

        private Result(object content)
        {
            Succeeded = true;
            ObjContent = content;
        }

        private Result(int content)
        {
            Succeeded = true;
            IntContent = content;
        }


        private Result(string content)
        {
            Succeeded = true;
            StrContent = content;
        }



        private Result(double content)
        {
            Succeeded = true;
            DblContent = content;
        }
        


        private Result(List<ValidationFailure> failures)
        {
            Failures = failures;
            Succeeded = false;
        }


        public static Result Success() => new();

        public static Result Success(int content) => new(content);

        public static Result Success(string content) => new(content);

        public static Result Success(double content) => new(content);

        public static Result Success(object content) => new(content);

        public static Result Failure(List<ValidationFailure> failures) => new(failures);
    }
}
