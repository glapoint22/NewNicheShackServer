﻿using FluentValidation.Results;

namespace Website.Application.Common.Classes
{
    public class Result
    {
        public bool Success { get; }
        public object? ObjContent { get; }
        public double? DblContent { get; }
        public string? StrContent { get; }
        public int? IntContent { get; }
        public List<ValidationFailure> Failures { get; } = new List<ValidationFailure>();


        private Result(bool success)
        {
            Success = success;
        }

        private Result(object? content)
        {
            Success = true;
            ObjContent = content;
        }

        private Result(int? content)
        {
            Success = true;
            IntContent = content;
        }


        private Result(string? content)
        {
            Success = true;
            StrContent = content;
        }



        private Result(double? content)
        {
            Success = true;
            DblContent = content;
        }
        


        private Result(List<ValidationFailure> failures)
        {
            Failures = failures;
            Success = false;
        }


        public static Result Succeeded() => new(true);

        public static Result Succeeded(int? content) => new(content);

        public static Result Succeeded(string? content) => new(content);

        public static Result Succeeded(double? content) => new(content);

        public static Result Succeeded(object? content) => new(content);

        public static Result Failed() => new(false);

        public static Result Failed(List<ValidationFailure> failures) => new(failures);

        public static Result Failed(List<KeyValuePair<string, string>> failures)
        {
            var validationFailures = new List<ValidationFailure>();

            foreach(var failure in failures)
            {
                validationFailures.Add(new ValidationFailure
                {
                    PropertyName = failure.Key,
                    ErrorMessage = failure.Value
                });
            }

            return new(validationFailures);
        }
    }
}