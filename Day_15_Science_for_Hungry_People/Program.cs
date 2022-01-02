using AdventOfCodeUtilities;

void P1(List<Ingredient> ingredients)
{
    Int64 score = Recurse(ingredients, new Dictionary<Ingredient, int>(), 100, false);
    Console.WriteLine(score);
    Console.ReadLine();
}

void P2(List<Ingredient> ingredients)
{
    Int64 score = Recurse(ingredients, new Dictionary<Ingredient, int>(), 100, true);
    Console.WriteLine(score);
    Console.ReadLine();
}



Int64 Recurse(List<Ingredient> remainingIngredients, Dictionary<Ingredient,int> quantities, int remainingTeaspoons, bool P2)
{
    Ingredient id = remainingIngredients[0];
    if (remainingIngredients.Count == 1)
    {
        quantities[id] = remainingTeaspoons;

        int totalCapacity = 0;
        int totalDurability = 0;
        int totalFlavour = 0;
        int totalTexture = 0;

        int totalCalories = 0;

        foreach (var kVP in quantities)
        {
            Ingredient ingredient = kVP.Key;
            int teaspoons = kVP.Value;

            totalCapacity += ingredient.Capacity * teaspoons;
            totalDurability += ingredient.Durability * teaspoons;
            totalFlavour += ingredient.Flavour * teaspoons;
            totalTexture += ingredient.Texture * teaspoons;

            totalCalories += ingredient.Calories * teaspoons;
        }

        totalCapacity = Math.Max(0, totalCapacity);
        totalDurability = Math.Max(0, totalDurability);
        totalFlavour = Math.Max(0, totalFlavour);
        totalTexture = Math.Max(0, totalTexture);

        Int64 score = 0;
        if (!P2 || totalCalories == 500)
            score = totalCapacity * totalDurability * totalFlavour * totalTexture;

        return score;
    }

    Int64 maxScore = Int64.MinValue;
    for (int teaspoons = 1; teaspoons <= remainingTeaspoons - (remainingIngredients.Count - 1); teaspoons++)
    {
        quantities[id] = teaspoons;

        Int64 score = Recurse(remainingIngredients.Where(x => x != id).ToList(), quantities, remainingTeaspoons - teaspoons, P2);
        maxScore = Math.Max(maxScore, score);
    }
    return maxScore;
}

List<string> inputList = AoCUtilities.GetInputLines();
List<Ingredient> ingredients = inputList.Select(x => new Ingredient(x)).ToList();

P1(ingredients);
P2(ingredients);

public class Ingredient
{
    public string Name;
    public int Capacity;
    public int Durability;
    public int Flavour;
    public int Texture;
    public int Calories;

    public Ingredient(string str)
    {
        string[] split = str.Split(' ');
        Name = split[0].Substring(0, split[0].Length - 1);
        Capacity = int.Parse(split[2].Substring(0, split[2].Length - 1));
        Durability = int.Parse(split[4].Substring(0, split[4].Length - 1));
        Flavour = int.Parse(split[6].Substring(0, split[6].Length - 1));
        Texture = int.Parse(split[8].Substring(0, split[8].Length - 1));
        Calories = int.Parse(split[10]);
    }

    public override string ToString()
    {
        return $"{Name} Capacity {Capacity} Durability {Durability} Flavour {Flavour} Texture {Texture} Calories {Calories}";
    }
}