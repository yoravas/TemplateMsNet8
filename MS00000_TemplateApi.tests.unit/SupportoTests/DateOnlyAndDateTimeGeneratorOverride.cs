using AutoBogus;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.SupportoTests;

public class DateOnlyAndDateTimeGeneratorOverride : AutoGeneratorOverride
{
    public override bool CanOverride(AutoGenerateContext context)
    {
        if (context.GenerateType == typeof(DateOnly) ||
            context.GenerateType == typeof(DateOnly?) ||
            context.GenerateType == typeof(DateTime) ||
            context.GenerateType == typeof(DateTime?))
        {
            return true;
        }
        else if ((context.ParentType == typeof(DateOnly) ||
                  context.ParentType == typeof(DateOnly?) ||
                  context.ParentType == typeof(TimeOnly) ||
                  context.ParentType == typeof(TimeOnly?)) &&
                  context.GenerateType == typeof(int))
        {
            if (context.GenerateName == nameof(DateOnly.Year).ToLower() ||
                context.GenerateName == nameof(DateOnly.Month).ToLower() ||
                context.GenerateName == nameof(DateOnly.Day).ToLower() ||
                context.GenerateName == nameof(DateTime.Hour).ToLower() ||
                context.GenerateName == nameof(DateTime.Minute).ToLower() ||
                context.GenerateName == nameof(DateTime.Second).ToLower() ||
                context.GenerateName == nameof(DateTime.Millisecond).ToLower())
            {
                return true;
            }
        }

        return false;
    }

    public override void Generate(AutoGenerateOverrideContext context)
    {
        switch (context.GenerateName)
        {
            case "year":
                context.Instance = context.Faker.Random.Int(1970, DateTime.Now.Year);

                break;
            case "month":
                context.Instance = context.Faker.Random.Int(1, 12);
                break;
            case "day":
                context.Instance = context.Faker.Random.Int(1, 28);
                break;
            case "hour":
                context.Instance = context.Faker.Random.Int(1, 23);
                break;
            case "minute":
                context.Instance = context.Faker.Random.Int(1, 59);
                break;
            case "second":
                context.Instance = context.Faker.Random.Int(1, 59);
                break;
            case "millisecond":
                context.Instance = context.Faker.Random.Int(1, 1000);
                break;
            default:

                if (context.GenerateType == typeof(DateOnly) || context.GenerateType == typeof(DateOnly?))
                {
                    DateTime from = new(1970, 7, 28);
                    DateTime to = DateTime.Now;
                    Faker faker = new();
                    DateTime date = faker.Date.Between(from, to);

                    context.Instance = DateOnly.FromDateTime(date);
                }
                else
                {
                    DateTime from = new(1970, 7, 28);
                    DateTime to = DateTime.Now;

                    Faker faker = new();
                    DateTime date = faker.Date.Between(from, to);

                    context.Instance = date;
                }

                break;
        }

    }
}
