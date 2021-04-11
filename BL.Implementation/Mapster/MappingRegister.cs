using BL.DTO;
using DAL.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace BL.Implementation.Mapster
{
    public class MappingRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTwoWays("[name]DTO", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ApplyDefaultRule();

            config.AdaptTwoWays("[name]DTO", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ForType<User>().IgnoreNoAttributes(typeof(DataMemberAttribute));

            config.GenerateMapper("[name]Mapper")
                .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(BaseEntity)), "DAL.Entities")
                .;
        }
    }

    internal static class RegisterExtensions
    {
        public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
        {
            return builder
                .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(BaseEntity)), "DAL.Entities")
                .ExcludeTypes(typeof(BaseEntity), typeof(User))
                .ExcludeTypes(type => type.IsEnum)
                //.AlterType(type => type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true, typeof(string))
                .ShallowCopyForSameType(true)
                .ForType<Consultation>(cfg => cfg.Ignore(c => c.Lecturer)
                                                 .Ignore(c => c.Subject))
                .ForType<Queue>(cfg =>
                { cfg.Ignore(q => q.Consultation);
                  cfg.Map(q => q.IssueCategory, typeof(IssueCategory));
                })
                .ForType<QueueMember>(cfg => cfg.Ignore(qm => qm.Student)
                                                .Ignore(qm => qm.Queue));
        }
    } 
}
