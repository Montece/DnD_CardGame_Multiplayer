using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

///<summary> Отвечает за подгрузку информации о карточках в паке (картинки, звуки, то что не синхронизируется) </summary>
public class CardsPack
{
    [SerializeField]
    public string ID;
    [SerializeField]
    public string Filename;
    [SerializeField]
    public string PackItemID;
    [SerializeField]
    private Dictionary<string, InfoCard> InfoCards = new Dictionary<string, InfoCard>();

    public CardsPack() {}

    public CardsPack(string id, string pack_item_id, string filename)
    {
        ID = id;
        Filename = filename;
        PackItemID = pack_item_id;
    }

    public void RegisterInfoCard(string title, string id, CardRarity cardRarity, BattleCard battleCard)
    {
        InfoCard infoCard = new InfoCard()
        {
            Title = title,
            Icon = Resources.Load<Sprite>("Cards/" + id),
            /*PlaceSound = Resources.Load<AudioClip>("Sounds/" + id + "_place"),
            AttackSound = Resources.Load<AudioClip>("Sounds/" + id + "_attack"),
            DeathSound = Resources.Load<AudioClip>("Sounds/" + id + "_death"),*/
            Rare = cardRarity,
            BattleReference = battleCard
        };

        InfoCards.Add(id, infoCard);
    }

    public Card GetRandomCard()
    {
        string id = InfoCards.Keys.ElementAt(Random.Range(0, InfoCards.Keys.Count));
        return Card.CreateCard(id, ID);
    }

    public InfoCard GetCardInfo(string id)
    {
        if (InfoCards.ContainsKey(id)) return InfoCards[id];
        else return null;
    }

    public IEnumerator LoadTextures()
    {
        foreach (string card_id in InfoCards.Keys)
        {
            InfoCard infoCard = InfoCards[card_id];

            using (var uwr = UnityWebRequestTexture.GetTexture("file://" + Path.Combine(Application.streamingAssetsPath, $"{Filename}\\Sprites\\{card_id}.png")))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.ProtocolError)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                    infoCard.Icon = sprite;
                }
            }
        }
    }

    /*public IEnumerator LoadSounds()
    {
        foreach (string card_id in InfoCards.Keys)
        {
            InfoCard infoCard = InfoCards[card_id];

            using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + Path.Combine(Application.streamingAssetsPath, $"{ID}\\Sounds\\{card_id}_place.mp3"), AudioType.MPEG))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.ProtocolError)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                    infoCard.PlaceSound = clip;
                }
            }

            using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + Path.Combine(Application.streamingAssetsPath, $"{ID}\\Sounds\\{card_id}_attack.mp3"), AudioType.MPEG))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.ProtocolError)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                    infoCard.AttackSound = clip;
                }
            }

            using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + Path.Combine(Application.streamingAssetsPath, $"{ID}\\Sounds\\{card_id}_death.mp3"), AudioType.MPEG))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.ProtocolError)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(uwr);
                    infoCard.DeathSound = clip;
                }
            }
        }
    }*/
}