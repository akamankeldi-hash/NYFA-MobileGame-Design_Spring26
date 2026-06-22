using System.Collections.Generic;

public static class PropDisplayNames
{
    private static readonly Dictionary<E_PropPiece, string> displayNames = new()
    {
        { E_PropPiece.BaseballBat, "baseball bat" },
        { E_PropPiece.Axe, "axe" },
        { E_PropPiece.Hammer, "hammer" },
        { E_PropPiece.Pistol, "pistol" },
        { E_PropPiece.MeatCleaver, "meat cleaver" },
        { E_PropPiece.Arrow, "arrow" },
        { E_PropPiece.Bottle, "bottle" },
        { E_PropPiece.Mug, "mug" },
        { E_PropPiece.Money, "stack of money" },
        { E_PropPiece.Book, "book" },
        { E_PropPiece.Burger, "burger" },
        { E_PropPiece.MedicalBag, "medical bag" },
        { E_PropPiece.HangmanRope, "rope" },
        { E_PropPiece.Necklace, "necklace" },
        { E_PropPiece.Eyeglasses, "pair of glasses" },
        { E_PropPiece.Tie, "tie" },
        { E_PropPiece.Crown, "crown" },
        { E_PropPiece.ChefHat, "chef hat" },
        { E_PropPiece.KidColoredHat, "colorful hat" },
        { E_PropPiece.Nimbus, "nimbus" },
        { E_PropPiece.DemonHorns, "horns" },
    };

    public static string Get(E_PropPiece prop) =>
        displayNames.TryGetValue(prop, out string name) ? name : prop.ToString();
}