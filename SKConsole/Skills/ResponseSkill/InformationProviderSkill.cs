using Microsoft.SemanticKernel.SkillDefinition;

namespace SemanticKernelDemo.Skills
{
    public class InformationProviderSkill
    {
        [SKFunction("補足情報を与える")]
        public string ProvideInfo(string input)
        {
            return $"{input}と言った人の名前は「ジョン・スミス」です。";
        }
    }
}
