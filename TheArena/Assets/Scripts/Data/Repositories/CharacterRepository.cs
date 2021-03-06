﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class BaseCharacterRepository : Repository
{
    public enum CharacterCode
    {
        [XmlEnum("MAIN")]
        MAIN,
        [XmlEnum("TEST_MAIN_1")]
        TEST_MAIN_1
    }

    private const string XML_PATH = "/BaseCharacters.xml";
    private static bool loaded = false;
    private static Dictionary<CharacterCode, BaseCharacter> characters = new Dictionary<CharacterCode, BaseCharacter>();

    private static void LoadCharacters()
    {
        if (!loaded)
        {
            XMLDataSerializer<BaseCharacterContainer> serializer 
                = new XMLDataSerializer<BaseCharacterContainer>(XML_PATH_PREFIX + XML_PATH);
            BaseCharacterContainer container = serializer.Deserialize();
            foreach(BaseCharacter character in container.BaseCharacters)
            {
                characters.Add(character.code, character);
            }
            loaded = true;
        }
    }

    public static BaseCharacter GetBaseCharacter(CharacterCode code)
    {
        LoadCharacters();
        return characters[code];
    }
}
