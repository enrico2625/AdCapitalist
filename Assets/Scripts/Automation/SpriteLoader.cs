using UnityEngine;

public static class SpriteLoader
{
    public static Sprite LoadSprite(string sheetPath, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(sheetPath);

        if (sprites == null || sprites.Length == 0)
        {
            return null;
        }

        foreach (Sprite s in sprites)
        {
            if (s.name == spriteName)
                return s;
        }
        return null;
    }
}

