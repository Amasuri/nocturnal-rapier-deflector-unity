using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    public enum SceneType
    {
        None,
        First,
        Second,
        ThirdBoss,
        EndGame,
    }

    static public string[] GetPreBattleDialogue(SceneType type)
    {
        switch (type)
        {
            case SceneType.First:
                return new string[]
{
                    "R: This is it.",
                    "R: This is what they call as The Forgotten Castle.",
                    "R: ..I smell something..",
                    "R: Rotten.",
                    "R: Smells like some food which was forgotten a hundred years ago!",
                    "R: ..Okay, where do I enter? I see something around the green.. thing. Ewww!",
                    "R: ..Wait? Who is that?..",
                    "W: ...",
                    "W: *flies away*",
                    "R: W-wait! Who are..",
                    "R: ..Ah!",
                    "R: They're throwing something at me!",
                    "R: There you go, my beloved rapier.. Let's get their attack right back at them!",
                    "R: (SHORT TUTORIAL)",
                    "R: (You have to deflect enemy attacks to hit them using your rapier with a mouse)",
                    "R: (You have 90 seconds to score a 3 to 1 hits ratio by deflecting attacks)",
                    "R: (You can press W to turn your rapier for either easier hitting or deflecting)",
                    "R: (The faster you move your mouse before the impact, the more energy is communicated to the deflected item)",
                    "R: (But don't move too fast if you want the hit to be registered)",
                    "R: (Good luck!)",
};

            case SceneType.Second:
                return new string[]
{
                    "R: ...",
                    "R: ..She ran away.",
                    "R: Phew. What was that?",
                    "R: Alright, onto the next room--",
                    "W: No, I won't tell you!",
                    "R: What?",
                    "R: Oh wait.",
                    "R: Are you, perhaps, the owner of this.. stinking place?",
                    "W: NO IT DOESN'T STINK!",
                    "W: Go awayyyyyy!",
                    "R: Another one..!",
};

            case SceneType.ThirdBoss:
                return new string[]
{
                    "R: Phewwww.",
                    "R: She really ran away now.",
                    "R: Guess I'll go to the next room..",
                    "R: They kinda look like cake or pie pieces in shape.",
                    "R: The top one on shaped weirdly, too..",
                    "R: The floor is all mushy and it stinks even more.",
                    "R: Gross.",
                    "W: Listen!",
                    "R: Ah--",
                    "W: You've come surprisingly deep into this castle of mine.",
                    "W: I challenge you to a duel!",
                    "W: If you beat me, I'll tell you the truth about this castle.",
                    "W: And then I'll throw you away using my most advanced magick.",
                    "R: And if I lose?",
                    "W: I'll just throw you away.",
                    "R: Oh.",
                    "W: I take that as a yes, so here we go!",
                    "R: Ack! She is serious!",
                    "R: Time to unsheathe my rapier!",
};

            case SceneType.EndGame:
                return new string[]
{
                    "R: ...",
                    "W: ...",
                    "R: ...",
                    "W: Fine, fine, I'll tell you!",
                    "R: Tell me what?..",
                    "W: This is my castle.",
                    "R: ...",
                    "R: Yeah, I get that.",
                    "W: This is also a cake castle.",
                    "W: Well.. it was.",
                    "W: Mow it stinks and have worms in it!",
                    "W: I don't know what to do.. ",
                    "W: I don't want to leave my castle, but I can't make it pretty again!",
                    "W: I also don't want to let others see it like this..",
                    "W: No one can help me..",
                    "R: ...",
                    "R: ..I am sorry. Swinging with my rapier is the only thing I am good at.",
                    "R: I can try to cut the worms out, but I think you'll need to make yourself another castle.",
                    "R: It can be even more beautiful than this one was once.",
                    "R: Or it can be an exact copy. Whatever you wish.",
                    "R: Living in eternal grief and scaring other people away isn't a happy lifestyle.",
                    "W: I know that..",
                    "W: What do I do..",
                    "R: Sometimes we just need a little push to do things.",
                    "R: A person, or an occasion.",
                    "R: ...",
                    "R: Okay. It's settled, then!",
                    "R: I'll be that push.",
                    "R: We will make a new castle together.",
                    "W: Thank you..",
                    "W: Thank you so much!",
};
        }

        return null;
    }

    static public string[] GetGameoverDialogue()
    {
        return new string[]
{
            "R: ..She is really strong.",
            "R: But I can do better.",
            "R: Let me do better.",
            "R: Let me take my chance!",
            "R: (Continue? Enter - yes, Backspace - no)",
};
    }
}
