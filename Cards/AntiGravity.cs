using System;

namespace DanModCards
{
    public class AntiGravity
    {
        public string Name { get; set; } = "Anti Gravity";
        public int GravityReduction { get; set; } = 100;

        public AntiGravity()
        {
            // Set gravity to 0%
            Console.WriteLine("Gravity has been reduced to 0%.");
        }

        public void ApplyEffect(Character character)
        {
            character.Gravity = 0;
            Console.WriteLine($"{character.Name}'s gravity is now {character.Gravity}.");
        }
    }
}