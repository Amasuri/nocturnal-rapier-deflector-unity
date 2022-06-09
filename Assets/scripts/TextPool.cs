using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextPool
{
    static public SceneLanguage sceneLang = SceneLanguage.Russian;
    static private Dictionary<SceneLanguage, Dictionary<SceneType, string[]>> ScenesByLanguage;
    public enum SceneLanguage
    {
        English,
        Russian
    }
    public enum SceneType
    {
        None,
        First,
        Second,
        ThirdBoss,
        WonGame,
        PlayerLost,
    }

    static TextPool()
    {
        ScenesByLanguage = new Dictionary<SceneLanguage, Dictionary<SceneType, string[]>> {

        //========English Text========
        { SceneLanguage.English, new Dictionary<SceneType, string[]>{
            { SceneType.First, new string[]
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
                    "R: (You have to deflect enemy attacks to hit them using your rapier with a mouse or your finger)",
                    "R: (You have one minute to score a 3 to 1 hits ratio by deflecting attacks)",
                    "R: (Don't worry about counting, the interface already tells you if you're winning or loosing, and how much)",
                    "R: (You can press W, or make a double tap to turn your rapier for easier deflecting)",
                    "R: (That's important! Certain attacks have a different angle, so it's better to act on them accordingly)",
                    "R: (The faster you move your mouse (or finger) before the impact, the more energy is communicated to the deflected item)",
                    "R: (But don't move too fast if you want the hit to be registered!)",
                    "R: (Move your sword gently, like a true master of the craft, unless you want your hits to backfire on you)",
                    "R: (Study witch's patterns, because she attacks differently in each battle)",
                    "R: (Good luck!)",
                    "R: ",
                    }
            },
            { SceneType.Second, new string[]
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
                    "R: ",
                    }
            },
            { SceneType.ThirdBoss, new string[]
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
                    "R: ",
                    }
            },
            { SceneType.WonGame, new string[]
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
                    "W: Mow it stinks and have wolms in it!",
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
                    "R: ",
                    }
            },
            { SceneType.PlayerLost, new string[]
                    {
                    "R: ..She is really strong.",
                    "R: But I can do better.",
                    "R: Let me do better.",
                    "R: Let me take my chance!",
                    "R: (Continue? Enter - yes, Backspace - no)",
                    }
            }
            }
            },

        //========Russian Text========

        { SceneLanguage.Russian, new Dictionary<SceneType, string[]>{
            { SceneType.First, new string[]
                    {
                    "R: �B���� �� �y �x�t�u����.",
                    "R: �M�u������, ������ �~�p�x���r�p���� �H�p�q�������} �H�p�}�{���}.",
                    "R: ..�X�u�}-���� ���p���~�u��..",
                    "R: �C�~�y�|�������z.",
                    "R: �H�p���p�� ���p�{���z, �q���t���� ������ ������������ �|�u�� �{�p�w�t���z �t�u�~�� ������-���� �s�~�y�|��.",
                    "R: ..�S�p�{, �s�t�u ������ �r�����t? �K�p�{�p��-���� �x�u�|�u�~�p�� �������{�u�~���y��... �U��!",
                    "R: ..�@? �K���� �x�t�u����?..",
                    "W: ...",
                    "W: *���|�u���p�u��*",
                    "R: �P-�����s���t�y! �S�� �{����..",
                    "R: ..�@�z!",
                    "R: �K�y�t�p�������� �~�u������������!",
                    "R: �S�p�{, �s�t�u ���p�} �}���� ���p���y���p.. �B���u�}�� �������p�w�p���� �p���p�{�y!",
                    "R: (�P�@�Q�@ �R�O�B�E�S�O�B)",
                    "R: (�X�����q�� �������p�x�y���� �p���p�{�y �r���p�s�p, �t�r�y�s�p�z���u �}�u�� �}�����{���z, �|�y�q�� ���p�|�����u�})",
                    "R: (�T �r�p�� �u������ ���{���|�� �}�y�~������, �������q�� �~�p�q���p���� ���������~�����u�~�y�u �����{���r 3 �{ 1)",
                    "R: (�N�u �q�u�������{���z���u���� ���� �����r���t�� �����t�����v���p �����{���r! �Y�{�p�|�p �r�~�y�x�� �����{�p�w�u��, �r���y�s�����r�p�u���u �|�y �r�� �y�|�y �~�u��, �y �~�p���{���|���{��)",
                    "R: (�N�p�w�}�y���u W �y�|�y ���p���~�y���u �t�r�p ���p�x�p, �������q�� �����r�u���~������ �}�u�� �y �|�u�s���u �������p�w�p���� �p���p�{�y �� �t�����s���s�� ���s�|�p)",
                    "R: (�^���� �r�p�w�~��! �K �~�u�{�����������} �p���p�{�p�} �~���w�u�~ ���r���z �����t�����t)",
                    "R: (�X�u�} �q���������u�u �r�� �t�r�y�s�p�u���u �}�����{���z �y�|�y ���p�|�����u�}, ���u�} ���y�|���~�u�u �������p�x�y������ ���~�p�����t)",
                    "R: (�N�� �~�u �t�r�y�s�p�z���u ���|�y���{���} �q����������, �y�~�p���u ���t�p�� �~�u �x�p�����y���p�u������!)",
                    "R: (�D�r�y�s�p�z���u �}�u�� �p�{�{�����p���~��, �{�p�{ �~�p�����������y�z �}�p�����u�� ���r���u�s�� �t�u�|�p, �u���|�y �~�u �������y���u, �������q�� �r�p���y �p���p�{�y ���t�p���y�|�y ���� �r�p�} �w�u)",
                    "R: (�I�x�����p�z���u �|���s�y�{�� �p���p�{�y �r�u�t���}��, ���p�{ �{�p�{ ���~�p �p���p�{���u�� ����-���p�x�~���}�� �r �{�p�w�t���} �����p�w�u�~�y�y)",
                    "R: (�T�t�p���y!)",
                    "R: ",
                    }
            },
            { SceneType.Second, new string[]
                    {
                    "R: ...",
                    "R: ..�O�~�p ���q�u�w�p�|�p.",
                    "R: �T����. �I ������ ������ �q���|��?",
                    "R: �S�p�{, ������ ���p�} �x�p �t�r�u������--",
                    "W: �N�u��, �� �~�y���u�s�� �~�u ���p�����{�p�w�� ���u�q�u!",
                    "R: �X����?",
                    "R: �O�z, �����s���t�y���u-�{�p.",
                    "R: �B�� ������, �r�|�p�t�u�|�y���p �������s��.. �x�|���r���~�~���s�� �}�u�����p?",
                    "W: �N�E�S �O�N�O �N�E �B�O�N�`�E�S!",
                    "W: �T�����t�y�y�y�y�y�y�y!",
                    "R: �O��������..!",
                    "R: ",
                    }
            },
            { SceneType.ThirdBoss, new string[]
                    {
                    "R: �T��������.",
                    "R: �S�u���u���� ���~�p �y �r�������}�� ���q�u�w�p�|�p.",
                    "R: �P���z�t��-�{�p �������}�������� ������ ���p�} �r ���|�u�t�������u�z �{���}�~�p���u..",
                    "R: �P�������w�u �~�p �{���������{�y ���������p �y�|�y ���y�����s�p �{�p�{���s��.",
                    "R: �B�u�����~�y�z �{�p�{���z-���� �������p�~�~���z �������}��..",
                    "R: �P���| �r�u���� �{�p�{ �{�p���y���p. �H�p���p�� �����r���p���y���u�|���~���z.",
                    "R: �P�����} ����.",
                    "W: �^�z!",
                    "R: �@--",
                    "W: �S�� �x�p���|�p �r ���p�}���u ���u���t���u �}���u�s�� �x�p�}�{�p.",
                    "W: �` �r���x���r�p�� ���u�q�� �~�p �t�����|��!",
                    "W: �E���|�y �����q�u�t�y���� �}�u�~��, �� ���p�����{�p�w�� ���u�q�u �r���� �����p�r�t�� �� �x�p�}�{�u.",
                    "W: �@ ���������} �r���{�y�~�� ���u�q�� �������q���z �}�p�s�y�u�z.",
                    "R: �@ �u���|�y �� �������y�s���p��?",
                    "W: �` ������������ ���u�q�� �r���{�y�~��.",
                    "R: �@.",
                    "W: �B�y�t�y�}�� ���� �����s�|�p���~�p, ���p�{ ������ �����|�����p�z!",
                    "R: �@�z! �K�p�{���z ���u�����v�x�~���z �~�p���������z!",
                    "R: �C�t�u �}���� ���p���y���p..!",
                    "R: ",
                    }
            },
            { SceneType.WonGame, new string[]
                    {
                    "R: ...",
                    "W: ...",
                    "R: ...",
                    "W: �L�p�t�~��, �|�p�t�~��. �S�p�{ ���w �y �q������. �Q�p�����{�p�w�� ���u�q�u!",
                    "R: �Q�p�����{�p�w�u����?..",
                    "W: �^���� �}���z �x�p�}���{.",
                    "R: ...",
                    "R: �N�� �t�p, �� �����~���|�p.",
                    "W: �B ���q���u�}, ������ ���������y�{���r���z �x�p�}���{. �I�x �����������r.",
                    "W: �N��.. �A���|.",
                    "W: �S���y���u���� ���~ �r�u�� �s�~�y�v�� �y �r ���u���r���{�p��!..",
                    "W: �` �~�u �x�~�p��, ������ �t�u�|�p����.. ",
                    "W: �` �~�u �������� �������p�r�|������ ���r���z �x�p�}���{, �~�� �� �~�u �}���s�� ���t�u�|�p���� �u�s�� �����u�{���p���~���} �r�~���r��..!",
                    "W: �@ �u���v �~�u ��������, �������q�� �t�����s�y�u �u�s�� ���p�{�y�} �r�y�t�u�|�y...",
                    "W: �N�y�{���� �}�~�u �~�u �����}���w�u��..",
                    "R: ...",
                    "R: ..�M�~�u �w�p�|��. �B���v ������ �� ���}�u�� - ������ �}�p���p���� ���p���y�����z.",
                    "R: �` �}���s�� �����}������ �� �r���t�v���s�y�r�p�~�y�u�} �r���u�� �����y�� ���u���r�u�z, �~�� �~���r���z �x�p�}���{ ���u�q�u �����y�t�v������ ���p�}���z �t�u�|�p����.",
                    "R: �B���x�}���w�~��, �~���r���z �q���t�u�� �u���v �q���|�u�u �����u�{���p���~���z, ���u�} �����p�����z �q���| �{���s�t�p-�|�y�q��.",
                    "R: �N��, �y�|�y �~���r���z �q���t�u�� �������~���z �{�����y�u�z. �K�p�{ �������u����, ���p�x���}�u�u������.",
                    "R: �G�y���� �r �r�u���~���} �s�����u �y ���������s�y�r�p���� �r���u��, �{���� �}�y�}�� �����������t�y�� - �~�u �����u�~�� �x�t�������r���z ���q���p�x �w�y�x�~�y..",
                    "W: �` �x�~�p��..",
                    "W: �N�� ������ �}�~�u �t�u�|�p����..",
                    "R: �I�~���s�t�p �~�p�} �~���w�u�~ �~�u�q���|�������z ���������{ �r �~���w�~���} �~�p�����p�r�|�u�~�y�y, �������q�� ������-���� ���t�u�|�p����.",
                    "R: �^���� �}���w�u�� �q������ ���u�|���r�u�{, �y�|�y �{�p�{���u-���� �����q�����y�u. �I�|�y �����u�t�|���s.",
                    "R: ...",
                    "R: �L�p�t�~��. �S�p�{ ���w �y �q������!",
                    "R: �` �����p�~�� �����y�} �������{���}.",
                    "R: �M�� ���t�u�|�p�u�} �~���r���z �x�p�}���{ �r�}�u�����u.",
                    "W: �R���p���y�q��..",
                    "W: �R���p���y�q�� ���s�����}�~���u!",
                    "R: ",
                    }
            },
            { SceneType.PlayerLost, new string[]
                    {
                    "R: ..�@ ���~�p ���y�|���~�p.",
                    "R: �N�� �� �����������q�~�p �~�p �q���|�����u�u.",
                    "R: �P���x�r���|�� �}�~�u ���t�u�|�p���� �q���|�����u�u.",
                    "R: �D�p�z �}�~�u ���p�~��!",
                    "R: (�P�����t���|�w�y����? Enter - �t�p, Backspace - �~�u��)",
                    }
            }
            }
            },
        };
    }

    static public string[] GetDialogue(SceneType type)
    {
        if(type == SceneType.None)
            return null;

        return ScenesByLanguage[sceneLang][type];
    }

    static public void SetSceneLanguage(SceneLanguage language)
    {
        sceneLang = language;
    }
}
