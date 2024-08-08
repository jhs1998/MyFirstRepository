namespace Builder
{
    internal class Program
    {
        // 빌더패턴 활용하기
        static void Main(string[] args)
        {
            //캐릭터 클래스 설정
            Characterclass civilianCharacterclass = new Characterclass();
            Characterclass BarbarianCharacterclass = new Characterclass();
            BarbarianCharacterclass
                .SetName("야만전사")
                .SetWeapon("도끼")
                .SetSkill("광분")
                .SetLevel(10)
                .SetHP(300);
            Characterclass bardCharacterclass = new Characterclass();
            bardCharacterclass
                .SetName("바드")
                .SetArmor("음악가의 앏은 옷")
                .SetSkill("바드의 고양감")
                .SetLevel(5)
                .SetMP(100);
            Characterclass sorcererCharacterclass = new Characterclass();
            sorcererCharacterclass
                .SetName("소서러")
                .SetWeapon("의식용 지팡이")
                .SetArmor("룬마법 망토")
                .SetSkill("주문 시전")
                .SetLevel(10)
                .SetHP(80)
                .SetMP(500);
            Characterclass rogueCharacterclass = new Characterclass();
            rogueCharacterclass
                .SetName("로그")
                .SetWeapon("독 발린 투척검")
                .SetArmor("은신 망토")
                .SetSkill("함정 해제")
                .SetLevel(8)
                .SetMP(20);

            Character[] characters = new Character[5];
            characters[0] = civilianCharacterclass.Build();
            characters[1] = BarbarianCharacterclass.Build();
            characters[2] = bardCharacterclass.Build();
            characters[3] = sorcererCharacterclass.Build();
            characters[4] = rogueCharacterclass.Build();

            // 설정값 출력
            for (int i = 0; i < characters.Length; i++)
            {
                Console.WriteLine($"{i+1}번 직업: {characters[i].name}");
                Console.WriteLine($" 무기: {characters[i].weapon} 방어구: {characters[i].armor} 레벨: {characters[i].level} 스킬: {characters[i].skill} HP: {characters[i].hp}  MP: {characters[i].mp}");
                Console.WriteLine();
            }
        }
    }

    public class Characterclass
    {
        public string name;
        public string weapon;
        public string armor;
        public string skill;
        public int level;
        public int hp;
        public int mp;

        public Characterclass()
        {
            name = "초보자";
            weapon = "몽둥이";
            armor = "천옷";
            skill = "달팽이 던지기";
            level = 1;
            hp = 100;
            mp = 0;
        }

        public Character Build()
        {
            Character character = new Character();
            character.name = name;
            character.weapon = weapon;
            character.armor = armor;
            character.skill = skill;
            character.level = level;
            character.hp = hp;
            character.mp = mp;
            return character;
        }

        public Characterclass SetName(string name)
        {
            this.name = name;
            return this;
        }

        public Characterclass SetWeapon(string weapon)
        {
            this.weapon = weapon;
            return this;
        }

        public Characterclass SetArmor(string armor)
        {
            this.armor = armor;
            return this;
        }
        public Characterclass SetSkill(string skill)
        {
            this.skill = skill;
            return this;
        }

        public Characterclass SetLevel(int level)
        {
            this.level = level;
            return this;
        }

        public Characterclass SetHP(int hp)
        {
            this.hp = hp;
            return this;
        }
        public Characterclass SetMP(int mp)
        {
            this.mp = mp;
            return this;
        }

    }

    public class Character
    {
        public string name;
        public string weapon;
        public string armor;
        public string skill;
        public int level;
        public int hp;
        public int mp;
    }
}
