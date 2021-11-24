// ------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// ------------------------------------------------

using Example.ConsoleApp.Models.Samples;
using Example.ConsoleApp.Models.Samples.Exceptions;
using System;

namespace Example.ConsoleApp.Services.Foundations.Samples
{
    public partial class SampleService
    {
        private static void ValidateSample(Sample sample)
        {
            ValidateSampleIsNull(sample);

            Validate(
               (Rule: IsInvalid(sample.Id), Parameter: nameof(Sample.Id)),
               (Rule: IsInvalid(sample.Text), Parameter: nameof(Sample.Text)));
        }

        private static void ValidateInput(int id) =>
            Validate((Rule: IsInvalid(id), Parameter: nameof(Sample.Id)));

        private static void ValidateSampleIsNull(Sample sample)
        {
            if (sample == null)
            {
                throw new NullSampleException();
            }
        }

        private static dynamic IsInvalid(int id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSampleException = new InvalidSampleException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSampleException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSampleException.ThrowIfContainsErrors();
        }
    }
}
