using System.Reflection.PortableExecutable;

namespace Factory_Method
{
    internal class Program
    {
        // 팩토리 메소드 패턴 활용하기
        static void Main(string[] args)
        {
            Skill fireball = SkillFactory.Instantiate("파이어볼");
            Skill lightning = SkillFactory.Instantiate("라이트닝");
            Skill steal = SkillFactory.Instantiate("훔치기");

            Skill[] skill = new Skill[3];
            skill[0] = fireball;
            skill[1] = lightning;
            skill[2] = steal;

            for (int i = 0; i < skill.Length; i++)
            {
                Console.WriteLine($"{skill[i].name}");
            }
        }
    }

    public class SkillFactory
    {
        public static Skill Instantiate(string name)
        {
            if (name == "파이어볼")
            {
                Magic magic = new Magic();
                magic.name = "파이어볼";
                magic.usemp = 10;
                magic.statuseffect = "화상";
                magic.explanation = "큰 화염구를 전방을 향해 날립니다.";
                magic.damage = 100;
                return magic;
            }
            else if (name == "라이트닝")
            {
                Magic magic = new Magic();
                magic.name = "라이트닝";
                magic.usemp = 100;
                magic.statuseffect = "감전";
                magic.explanation = "벼락을 불러 상대를 공격합니다.";
                magic.damage = 300;
                return magic;
            }
            else if (name == "훔치기")
            {
                Technique technique = new Technique();
                technique.name = "훔치기";
                technique.useap = 30;
                technique.explanation = "손기술로 상대의 소지품을 훔칩니다.";
                return technique;
            }
            else
            {
                Console.WriteLine("해당 이름의 스킬이 없습니다.");
                return null;
            }
        }
    }

    public class Skill
    {
        public string name;      
        public string explanation;
    }

    public class Magic : Skill
    {
        public string statuseffect;
        public int usemp;
        public int damage;
    }
    public class Technique : Skill
    {
        public int useap;
    }
}
