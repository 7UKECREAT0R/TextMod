using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using DiscordRPC;
using System.Reflection;
using System.Collections.Specialized;

namespace TextMod
{
    public partial class Form1 : Form
    {
        static readonly string VERSION = "1.13";
        static readonly string[] Changelog =
        {
            "Mentions now work properly and aren't immediately sent.",
            "Added -react!"
        };
        readonly Stopwatch sw;

        int mentionKeys = 0;
        bool bypass = false;
        bool performance = false;
        bool bubbleshown = false;
        //bool slowpc = false;
        string lastText;
        /// <summary>
        /// The sample of character data that represents the zalgo template.
        /// </summary>
        private static readonly string zalgoSample = "a͕̯̻̰͚͓̱̋̅̀̀̓̅͊̓͐b̵͔̭̙̥͙̤̲̜̥́̈͗̇͑̐̎́̔̈́c̗̰͙̦̔̀̂̍̌̋͘͢͢ď̡̰͚̙̬̼̞͍͒̂̉͂̊̎̅͝͝ȇ̷̢̳̙̟̼͖̜̙̔̿̆̈̒̅͋̑̐͜f̜̣͙̩̬͚̫̞̮͊̎̊́͒̌̾͟͠͠g̷̢̨͕̬̺̟̘̥͔̥͌͐̿̏͠h̢͇̪̦̦̩̻̣̍̅͌̌̿́̕i͔̯̳̝͔̮͙̗͈͌̓̾̏͐͘̚͢j̡̧͙̗͓͈̔̇̍́̔̕k̵̢̗̰͙̖͇̙̫̾͊̋̊͋̄̕͡l̷̺̺̱̩̙̟̞̥̪̏̋̑̆̀̑̎́̌̚ͅm̡̧̢̲̖̯̝̭̤͐͆̾̆͞͡n̲̥͕̙̩̟̺̒͛͒͒͆̚͢ơ̛̬̠͉̜͕͊̂͛̾̋̎͒͘p̵̨̜̫͚̰̌̃͗̇̇͘͜͢͜͝q̸̧̬̺̝̹͉̮̮̫͐̇̀̂̈͢ř̨̯̱̰͍̙̝͐̑̓̏̌͒̉̑ş̴̢̱̫͓͈͔͂͂̔̔̓̓̏͂̚͡ͅt̢̗͈̮̣͚̤̖̂͒̏͑͊̄̅̾͡͠ų̵̨͇̻̮̤͆̑̌͘̕͞v̡͙̰̞̲͙̜̖̍̐́̀͑͘͜͠w̶̧̺̦̣͈̝̆̆̀͛̄x̸̳͍͇̞̬͉̗̞̖͒̃̈́̀̒͑ͅy̧̧̖͓̾́͘͜͠͠ẕ̶̭͚̥͔̥̣̖͊̉̀̐̀̓̐͜͝͝͡ͅȂ̸̧̝̬̱̱̮̱͇̭̿̍̊͗͐̃̚͝͞ͅB̡̛͓͈͔̪̦̜̭̤́̄͛̆̂̌͢Ċ̨̖͔͎̦̥́͛͂̊̓̚͠Ḏ̶͈̲̪̓͒̽̾̎͢͜͞E̢̖͔̞͇͌̉̇́͋̔̒́̾͘F̴̛̖̯̻̝̃̈́͑̄̉̓̉͢͟͢Ĝ̵̰̥̺̱͙͚̞̰̟̊̑̊͡H̨̪̟̥̺̰̪͚̟̗̾̿̀̈́͛͐̎͝I̴̧͉̳͎̟̱̻̗̒̑̈́̎̀͛͝ͅJ̠̤͖͎͕̦͗͌̉̐͜͠K̖̳̮͙͚̻̰͔̈́̊́͛͛̾̓͂́͢L̷̙̮̜͈̤̭͎͎̳̍͛̊͋̂̀M̶̖̲̰̫͙̳̳̙̥̿̈́͛̈̈̐͜N̫͉̰̣͔̤͎̆̑̒̃̾̀͠͞ͅƠ̸̢̪͔̱̥͒̿̉̀̉̐̕͜͞P̴̼̻̲̗̯̲̋̔̑̓͡͡͡Q̵̡̝̦̗̼͙͖̫̰͗̑͋̌̒̽́̈R̷̛̞̱̻̫̮̭̆̊̽̂͛͑͢Ş̨̪͙̣̩̤̻͛̀̾̄̕T̙͈̪̲͔͚̑͑́̇̇̄͘͘͟͝ͅU̖͓͈̰̹̦̠̐͗̀̕͜͝͠V̫͈̗̘̘̦̾̎̃͂̚͠W̡̠̬͉͕̮͑̎̑͐̿͐͒͑̚̕X̸̨͈̪̞̮̟͈͕͛̽̈́̓̊̄Y̨̧̛̝̝̟͍̪̏̊̊͌͢͠͞Z̷̧̛̩̯̜̤̗̀̌͆̉̎͢͢͞1̨͎͖̗̪̜̉̀̂͑̾̚͜͞͞2̸̗͚̻̩̰̹̟̫̽̔͌̃̕͜͠3̷͍̬̗͕̦̺̺͉̊̃̍͂́̑̚͟͞4̧̢̣̩͖̎͗̑̀́̎̄͊̒͟5̸̭͇͚̫̝̖̏͗̒̊͠6̷̢̢̖͇͕̹̮͕̈́͆͆́̋̿̚͝͡7̸̼̤̲̰̠͖̄̀͊͆͞͝8͎̱̯̹͔͖̰̣͊͌̓̋̚͟͠͠͠ͅ9̵̖͇̩̬̺̪͈̖̪͒̿̾̉̾͛̈̂̀̕0̷̛̫̳͔̲͂̿͗̐́̇̚͟";
        // initialized at the end of constructor
        public static string[] zalgo;
        public static char[] alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static char[] rawalphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] gtag = "abcdefghijklmnopqrstuvwxyzA8CD3F6H1JK7MN0PQR5TUVWXYZI2E4SGLB9O".ToCharArray();
        private static readonly char[] small = "ᴀʙᴄᴅᴇғɢʜɪᴊᴋʟᴍɴᴏᴘǫʀsᴛᴜᴠᴡxʏᴢABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private static readonly char[] superscript = "ᵃᵇᶜᵈᵉᶠᵍʰᶦʲᵏˡᵐⁿᵒᵖᵠʳˢᵗᵘᵛʷˣʸᶻᴬᴮᶜᴰᴱᶠᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾᵠᴿˢᵀᵁⱽᵂˣʸᶻ¹²³⁴⁵⁶⁷⁸⁹⁰".ToCharArray();
        public static Dictionary<char, string> nato = new Dictionary<char, string>();
        public static Dictionary<char, string> sounds = new Dictionary<char, string>();
        readonly Dictionary<string, string> emojis = new Dictionary<string, string>();
        readonly EmojiStringProvider stringProvider;
        public static Keys processhotkey = Keys.Enter;
        public static List<IntPtr> fontMemoryToBeDeallocated = new List<IntPtr>();
        public Form1()
        {
            InitializeComponent();

            // Title.Text = "TextMod " + VERSION;
            // origLocation = Title.Location;

            // Takes FOREVER...
            stringProvider = new EmojiStringProvider();

            // For the hover stuffs.
            sw = new Stopwatch();
            sw.Start();
            timer1.Interval = 1;
            timer1.Start();

            // round that boi
            FormBorderStyle = FormBorderStyle.None;
            SetBorderCurve(30);

            // discord rpc
            DiscordRpcClient drpc = new DiscordRpcClient("621341181925654545");
            drpc.Initialize();
            ImageGenerator.singleton = drpc;

            zalgo = zalgoSample.Split(alphabet, StringSplitOptions.RemoveEmptyEntries);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string APPNAME = "TextMod";
            bool exists = rk.GetValue(APPNAME) != null;
            if(!exists)
            {
                rk.SetValue("TextMod", Application.ExecutablePath);
                MessageBox.Show("Textmod will start up on windows launch.\n" +
                    "To disable this at any time, simply disable it in \"Task Manager -> Startup\".", "Installer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            toolTip1.SetToolTip(bypassCheckbox, "When this is on, a blank message is sent and then edited to avoid being warned by moderator bots.");
            toolTip1.SetToolTip(panel1, "When this is on, a blank message is sent and then edited to avoid being warned by moderator bots.");

            toolTip1.SetToolTip(performanceModeButton, "Puts a delay between inputs, making textmod work on slower pcs.\nEnable this if F12 is not working most of the time!");

            toolTip1.SetToolTip(button2, "Version information, changelog, credits, etc...");

            toolTip1.SetToolTip(button1, "Opens the folder textmod uses for emojis.\nClick the \"Custom Emoji\" button to get help on how to use.");

            toolTip1.SetToolTip(activatorChanger, "Change the key you use to activate textmod. This is F12 by default.");

            toolTip1.SetToolTip(minimize, "Minimizes TextMod to the hidden icons folder.");
            toolTip1.SetToolTip(exit, "Exits TextMod.");

            AddFontFromResource(Program.pfc, "Whitney Medium.ttf");
            AddFontFromResource(Program.pfc, "ostrich-rounded.ttf");
            AddFontFromResource(Program.pfc, "Roboto-Regular.ttf");

            foreach(Control c in Controls)
            {
                Font newFont;
                Font f = c.Font;
                newFont = new Font(Program.GetPrivateFont
                    (Program.PrvFont.TextModFont), f.Size, f.Style);
                f.Dispose();
                c.Font = newFont;
            }
            Keys saved = processhotkey;
            if (!Directory.Exists("textModTempData"))
                Directory.CreateDirectory("textModTempData");

            if (File.Exists("textModTempData\\textmodHotkey.bruh"))
            {
                string nm = File.ReadAllText("textModTempData\\textmodHotkey.bruh");
                foreach(Keys k in Enum.GetValues(typeof(Keys)))
                {
                    if(k.ToString().Equals(nm))
                    {
                        saved = k;
                        break;
                    }
                }
            }
            processhotkey = saved;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if(!Directory.Exists("textModData")) {
                Directory.CreateDirectory("textModData");
            }
            if (!Directory.Exists("textModData\\text")) {
                Directory.CreateDirectory("textModData\\text");
            }
            if (!Directory.Exists("textModData\\scripts")) {
                Directory.CreateDirectory("textModData\\scripts");
            }
            _Kproc = KeyboardHookCallback;
            _keyboardHookID = SetKeyboardHook(_Kproc);

            notifyIcon.Icon = SystemIcons.Application;

            nato.Add('a', "Alfa");
            nato.Add('b', "Bravo");
            nato.Add('c', "Charlie");
            nato.Add('d', "Delta");
            nato.Add('e', "Echo");
            nato.Add('f', "Foxtrot");
            nato.Add('g', "Golf");
            nato.Add('h', "Hotel");
            nato.Add('i', "India");
            nato.Add('j', "Juliett");
            nato.Add('k', "Kilo");
            nato.Add('l', "Lima");
            nato.Add('m', "Mike");
            nato.Add('n', "November");
            nato.Add('o', "Oscar");
            nato.Add('p', "Papa");
            nato.Add('q', "Quebec");
            nato.Add('r', "Romeo");
            nato.Add('s', "Sierra");
            nato.Add('t', "Tango");
            nato.Add('u', "Uniform");
            nato.Add('v', "Victor");
            nato.Add('w', "Whiskey");
            nato.Add('x', "Xray");
            nato.Add('y', "Yankee");
            nato.Add('z', "Zulu");
            Extensions.nato = nato;

            sounds.Add('a', "ay");
            sounds.Add('b', "bee");
            sounds.Add('c', "cee");
            sounds.Add('d', "dee");
            sounds.Add('e', "ee");
            sounds.Add('f', "ef");
            sounds.Add('g', "gee");
            sounds.Add('h', "aych");
            sounds.Add('i', "ai");
            sounds.Add('j', "jay");
            sounds.Add('k', "kay");
            sounds.Add('l', "el");
            sounds.Add('m', "em");
            sounds.Add('n', "en");
            sounds.Add('o', "oh");
            sounds.Add('p', "pee");
            sounds.Add('q', "cue");
            sounds.Add('r', "are");
            sounds.Add('s', "ess");
            sounds.Add('t', "tee");
            sounds.Add('u', "you");
            sounds.Add('v', "vee");
            sounds.Add('w', "dabulyou");
            sounds.Add('x', "ecks");
            sounds.Add('y', "why");
            sounds.Add('z', "zee");
            Extensions.sounds = sounds;

            try
            {
                emojis.Add("grinning", ":grinning:");
                emojis.Add("grin", ":grin:");
                emojis.Add("joy", ":joy:");
                emojis.Add("rofl", ":rofl:");
                emojis.Add("smiley", ":smiley:");
                emojis.Add("smile", ":smile:");
                emojis.Add("sweat_smile", ":sweat_smile:");
                emojis.Add("laughing", ":laughing:");
                emojis.Add("wink", ":wink:");
                emojis.Add("blush", ":blush:");
                emojis.Add("yum", ":yum:");
                emojis.Add("sunglasses", ":sunglasses:");
                emojis.Add("heart_eyes", ":heart_eyes:");
                emojis.Add("kissing_heart", ":kissing_heart:");
                emojis.Add("kissing", ":kissing:");
                emojis.Add("kissing_smiling_eyes", ":kissing_smiling_eyes:");
                emojis.Add("kissing_closed_eyes", ":kissing_closed_eyes:");
                emojis.Add("relaxed", ":relaxed:");
                emojis.Add("slight_smile", ":slight_smile:");
                emojis.Add("hugging", ":hugging:");
                emojis.Add("thinking", ":thinking:");
                emojis.Add("neutral_face", ":neutral_face:");
                emojis.Add("expressionless", ":expressionless:");
                emojis.Add("no_mouth", ":no_mouth:");
                emojis.Add("rolling_eyes", ":rolling_eyes:");
                emojis.Add("smirk", ":smirk:");
                emojis.Add("persevere", ":persevere:");
                emojis.Add("disappointed_relieved", ":disappointed_relieved:");
                emojis.Add("open_mouth", ":open_mouth:");
                emojis.Add("zipper_mouth", ":zipper_mouth:");
                emojis.Add("hushed", ":hushed:");
                emojis.Add("sleepy", ":sleepy:");
                emojis.Add("tired_face", ":tired_face:");
                emojis.Add("sleeping", ":sleeping:");
                emojis.Add("relieved", ":relieved:");
                emojis.Add("stuck_out_tongue", ":stuck_out_tongue:");
                emojis.Add("stuck_out_tongue_winking_eye", ":stuck_out_tongue_winking_eye:");
                emojis.Add("stuck_out_tongue_closed_eyes", ":stuck_out_tongue_closed_eyes:");
                emojis.Add("drooling_face", ":drooling_face:");
                emojis.Add("unamused", ":unamused:");
                emojis.Add("sweat", ":sweat:");
                emojis.Add("pensive", ":pensive:");
                emojis.Add("confused", ":confused:");
                emojis.Add("upside_down", ":upside_down:");
                emojis.Add("money_mouth", ":money_mouth:");
                emojis.Add("astonished", ":astonished:");
                emojis.Add("frowning2 ", ":frowning2:");
                emojis.Add("slight_frown", ":slight_frown:");
                emojis.Add("confounded", ":confounded:");
                emojis.Add("disappointed", ":disappointed:");
                emojis.Add("worried", ":worried:");
                emojis.Add("triumph", ":triumph:");
                emojis.Add("cry", ":cry:");
                emojis.Add("sob", ":sob:");
                emojis.Add("frowning", ":frowning:");
                emojis.Add("anguished", ":anguished:");
                emojis.Add("fearful", ":fearful:");
                emojis.Add("weary", ":weary:");
                emojis.Add("grimacing", ":grimacing:");
                emojis.Add("cold_sweat", ":cold_sweat:");
                emojis.Add("scream", ":scream:");
                emojis.Add("flushed", ":flushed:");
                emojis.Add("dizzy_face", ":dizzy_face:");
                emojis.Add("rage", ":rage:");
                emojis.Add("angry", ":angry:");
                emojis.Add("mask", ":mask:");
                emojis.Add("thermometer_face", ":thermometer_face:");
                emojis.Add("head_bandage", ":head_bandage:");
                emojis.Add("nauseated_face", ":nauseated_face:");
                emojis.Add("sneezing_face", ":sneezing_face:");
                emojis.Add("innocent", ":innocent:");
                emojis.Add("cowboy", ":cowboy:");
                emojis.Add("clown", ":clown:");
                emojis.Add("lying_face", ":lying_face:");
                emojis.Add("nerd", ":nerd:");
                emojis.Add("smiling_imp", ":smiling_imp:");
                emojis.Add("imp", ":imp:");
                emojis.Add("japanese_ogre", ":japanese_ogre:");
                emojis.Add("japanese_goblin", ":japanese_goblin:");
                emojis.Add("skull", ":skull:");
                emojis.Add("ghost", ":ghost:");
                emojis.Add("alien", ":alien:");
                emojis.Add("robot", ":robot:");
                emojis.Add("poop", ":poop:");
                emojis.Add("smiley_cat", ":smiley_cat:");
                emojis.Add("smile_cat", ":smile_cat:");
                emojis.Add("joy_cat", ":joy_cat:");
                emojis.Add("heart_eyes_cat", ":heart_eyes_cat:");
                emojis.Add("smirk_cat", ":smirk_cat:");
                emojis.Add("kissing_cat", ":kissing_cat:");
                emojis.Add("scream_cat", ":scream_cat:");
                emojis.Add("crying_cat_face", ":crying_cat_face:");
                emojis.Add("pouting_cat", ":pouting_cat:");
                emojis.Add("baby", ":baby:");
                emojis.Add("girl", ":girl:");
                emojis.Add("boy", ":boy:");
                emojis.Add("woman", ":woman:");
                emojis.Add("man", ":man:");
                emojis.Add("cop", ":cop:");
                emojis.Add("construction_worker ", ":construction_worker:");
                emojis.Add("guardsman ", ":guardsman:");
                emojis.Add("spy", ":spy:");
                emojis.Add("bride_with_veil", ":bride_with_veil:");
                emojis.Add("man_in_tuxedo", ":man_in_tuxedo:");
                emojis.Add("princess", ":princess:");
                emojis.Add("prince", ":prince:");
                emojis.Add("mrs_claus", ":mrs_claus:");
                emojis.Add("santa", ":santa:");
                emojis.Add("angel", ":angel:");
                emojis.Add("pregnant_woman", ":pregnant_woman:");
                emojis.Add("nail_care", ":nail_care:");
                emojis.Add("selfie", ":selfie:");
                emojis.Add("dancer", ":dancer:");
                emojis.Add("man_dancing", ":man_dancing:");
                emojis.Add("couple", ":couple:");
                emojis.Add("two_women_holding_hands", ":two_women_holding_hands:");
                emojis.Add("two_men_holding_hands", ":two_men_holding_hands:");
                emojis.Add("couple_with_heart", ":couple_with_heart:");
                emojis.Add("couple_ww", ":couple_ww:");
                emojis.Add("couple_mm", ":couple_mm:");
                emojis.Add("couplekiss", ":couplekiss:");
                emojis.Add("kiss_ww", ":kiss_ww:");
                emojis.Add("kiss_mm", ":kiss_mm:");
                emojis.Add("family", ":family:");
                emojis.Add("family_mwg", ":family_mwg:");
                emojis.Add("family_mwgb", ":family_mwgb:");
                emojis.Add("family_mwbb", ":family_mwbb:");
                emojis.Add("family_mwgg", ":family_mwgg:");
                emojis.Add("family_wwb", ":family_wwb:");
                emojis.Add("family_wwg", ":family_wwg:");
                emojis.Add("family_wwgb", ":family_wwgb:");
                emojis.Add("family_wwbb", ":family_wwbb:");
                emojis.Add("family_wwgg", ":family_wwgg:");
                emojis.Add("family_mmb", ":family_mmb:");
                emojis.Add("family_mmg", ":family_mmg:");
                emojis.Add("family_mmgb", ":family_mmgb:");
                emojis.Add("family_mmbb", ":family_mmbb:");
                emojis.Add("family_mmgg", ":family_mmgg:");
                emojis.Add("woman  boy", ":woman::boy:");
                emojis.Add("woman  girl", ":woman::girl:");
                emojis.Add("woman  girl  boy", ":woman::girl::boy:");
                emojis.Add("woman  boy  boy", ":woman::boy::boy:");
                emojis.Add("woman  girl  girl", ":woman::girl::girl:");
                emojis.Add("man  boy", ":man::boy:");
                emojis.Add("man  girl", ":man::girl:");
                emojis.Add("man  girl  boy", ":man::girl::boy:");
                emojis.Add("man  boy  boy", ":man::boy::boy:");
                emojis.Add("man  girl  girl", ":man::girl::girl:");
                emojis.Add("open_hands", ":open_hands:");
                emojis.Add("raised_hands", ":raised_hands:");
                emojis.Add("clap", ":clap:");
                emojis.Add("handshake", ":handshake:");
                emojis.Add("thumbsup", ":thumbsup:");
                emojis.Add("thumbsdown", ":thumbsdown:");
                emojis.Add("punch", ":punch:");
                emojis.Add("fist", ":fist:");
                emojis.Add("left_facing_fist", ":left_facing_fist:");
                emojis.Add("right_facing_fist", ":right_facing_fist:");
                emojis.Add("fingers_crossed", ":fingers_crossed:");
                emojis.Add("v", ":v:");
                emojis.Add("metal", ":metal:");
                emojis.Add("ok_hand", ":ok_hand:");
                emojis.Add("point_left", ":point_left:");
                emojis.Add("point_right", ":point_right:");
                emojis.Add("point_up_2", ":point_up_2:");
                emojis.Add("dog", ":dog:");
                emojis.Add("cat", ":cat:");
                emojis.Add("mouse", ":mouse:");
                emojis.Add("hamster", ":hamster:");
                emojis.Add("rabbit", ":rabbit:");
                emojis.Add("fox", ":fox:");
                emojis.Add("bear", ":bear:");
                emojis.Add("panda_face", ":panda_face:");
                emojis.Add("koala", ":koala:");
                emojis.Add("tiger", ":tiger:");
                emojis.Add("lion_face", ":lion_face:");
                emojis.Add("cow", ":cow:");
                emojis.Add("pig", ":pig:");
                emojis.Add("pig_nose", ":pig_nose:");
                emojis.Add("frog", ":frog:");
                emojis.Add("monkey_face", ":monkey_face:");
                emojis.Add("see_no_evil", ":see_no_evil:");
                emojis.Add("hear_no_evil", ":hear_no_evil:");
                emojis.Add("speak_no_evil", ":speak_no_evil:");
                emojis.Add("monkey", ":monkey:");
                emojis.Add("chicken", ":chicken:");
                emojis.Add("penguin", ":penguin:");
                emojis.Add("bird", ":bird:");
                emojis.Add("baby_chick", ":baby_chick:");
                emojis.Add("hatching_chick", ":hatching_chick:");
                emojis.Add("hatched_chick", ":hatched_chick:");
                emojis.Add("duck", ":duck:");
                emojis.Add("eagle", ":eagle:");
                emojis.Add("owl", ":owl:");
                emojis.Add("bat", ":bat:");
                emojis.Add("wolf", ":wolf:");
                emojis.Add("boar", ":boar:");
                emojis.Add("horse", ":horse:");
                emojis.Add("unicorn", ":unicorn:");
                emojis.Add("bee", ":bee:");
                emojis.Add("bug", ":bug:");
                emojis.Add("butterfly", ":butterfly:");
                emojis.Add("snail", ":snail:");
                emojis.Add("shell", ":shell:");
                emojis.Add("beetle", ":beetle:");
                emojis.Add("ant", ":ant:");
                emojis.Add("spider", ":spider:");
                emojis.Add("spider_web", ":spider_web:");
                emojis.Add("scorpion", ":scorpion:");
                emojis.Add("turtle", ":turtle:");
                emojis.Add("snake", ":snake:");
                emojis.Add("lizard", ":lizard:");
                emojis.Add("octopus", ":octopus:");
                emojis.Add("squid", ":squid:");
                emojis.Add("shrimp", ":shrimp:");
                emojis.Add("crab", ":crab:");
                emojis.Add("blowfish", ":blowfish:");
                emojis.Add("tropical_fish", ":tropical_fish:");
                emojis.Add("fish", ":fish:");
                emojis.Add("dolphin", ":dolphin:");
                emojis.Add("whale", ":whale:");
                emojis.Add("whale2", ":whale2:");
                emojis.Add("shark", ":shark:");
                emojis.Add("crocodile", ":crocodile:");
                emojis.Add("tiger2", ":tiger2:");
                emojis.Add("leopard", ":leopard:");
                emojis.Add("gorilla", ":gorilla:");
                emojis.Add("elephant", ":elephant:");
                emojis.Add("rhino", ":rhino:");
                emojis.Add("dromedary_camel", ":dromedary_camel:");
                emojis.Add("camel", ":camel:");
                emojis.Add("water_buffalo", ":water_buffalo:");
                emojis.Add("ox", ":ox:");
                emojis.Add("cow2", ":cow2:");
                emojis.Add("racehorse", ":racehorse:");
                emojis.Add("pig2", ":pig2:");
                emojis.Add("ram", ":ram:");
                emojis.Add("sheep", ":sheep:");
                emojis.Add("goat", ":goat:");
                emojis.Add("deer", ":deer:");
                emojis.Add("dog2", ":dog2:");
                emojis.Add("poodle", ":poodle:");
                emojis.Add("cat2", ":cat2:");
                emojis.Add("rooster", ":rooster:");
                emojis.Add("turkey", ":turkey:");
                emojis.Add("dove", ":dove:");
                emojis.Add("rabbit2", ":rabbit2:");
                emojis.Add("mouse2", ":mouse2:");
                emojis.Add("rat", ":rat:");
                emojis.Add("chipmunk", ":chipmunk:");
                emojis.Add("feet", ":feet:");
                emojis.Add("dragon", ":dragon:");
                emojis.Add("dragon_face", ":dragon_face:");
                emojis.Add("cactus", ":cactus:");
                emojis.Add("christmas_tree", ":christmas_tree:");
                emojis.Add("evergreen_tree", ":evergreen_tree:");
                emojis.Add("deciduous_tree", ":deciduous_tree:");
                emojis.Add("palm_tree", ":palm_tree:");
                emojis.Add("seedling", ":seedling:");
                emojis.Add("herb", ":herb:");
                emojis.Add("shamrock ", ":shamrock:");
                emojis.Add("four_leaf_clover", ":four_leaf_clover:");
                emojis.Add("bamboo", ":bamboo:");
                emojis.Add("tanabata_tree", ":tanabata_tree:");
                emojis.Add("leaves", ":leaves:");
                emojis.Add("fallen_leaf", ":fallen_leaf:");
                emojis.Add("maple_leaf", ":maple_leaf:");
                emojis.Add("mushroom", ":mushroom:");
                emojis.Add("ear_of_rice", ":ear_of_rice:");
                emojis.Add("bouquet", ":bouquet:");
                emojis.Add("tulip", ":tulip:");
                emojis.Add("rose", ":rose:");
                emojis.Add("wilted_rose", ":wilted_rose:");
                emojis.Add("hibiscus", ":hibiscus:");
                emojis.Add("cherry_blossom", ":cherry_blossom:");
                emojis.Add("blossom", ":blossom:");
                emojis.Add("sunflower", ":sunflower:");
                emojis.Add("sun_with_face", ":sun_with_face:");
                emojis.Add("full_moon_with_face", ":full_moon_with_face:");
                emojis.Add("first_quarter_moon_with_face", ":first_quarter_moon_with_face:");
                emojis.Add("last_quarter_moon_with_face", ":last_quarter_moon_with_face:");
                emojis.Add("new_moon_with_face", ":new_moon_with_face:");
                emojis.Add("full_moon", ":full_moon:");
                emojis.Add("waning_gibbous_moon", ":waning_gibbous_moon:");
                emojis.Add("last_quarter_moon", ":last_quarter_moon:");
                emojis.Add("waning_crescent_moon", ":waning_crescent_moon:");
                emojis.Add("new_moon", ":new_moon:");
                emojis.Add("waxing_crescent_moon", ":waxing_crescent_moon:");
                emojis.Add("first_quarter_moon", ":first_quarter_moon:");
                emojis.Add("waxing_gibbous_moon", ":waxing_gibbous_moon:");
                emojis.Add("crescent_moon", ":crescent_moon:");
                emojis.Add("earth_americas", ":earth_americas:");
                emojis.Add("earth_africa", ":earth_africa:");
                emojis.Add("earth_asia", ":earth_asia:");
                emojis.Add("dizzy", ":dizzy:");
                emojis.Add("star ", ":star:");
                emojis.Add("star2", ":star2:");
                emojis.Add("sparkles", ":sparkles:");
                emojis.Add("zap ", ":zap:");
                emojis.Add("comet ", ":comet:");
                emojis.Add("boom", ":boom:");
                emojis.Add("fire", ":fire:");
                emojis.Add("cloud_tornado", ":cloud_tornado:");
                emojis.Add("rainbow", ":rainbow:");
                emojis.Add("sunny ", ":sunny:");
                emojis.Add("white_sun_small_cloud", ":white_sun_small_cloud:");
                emojis.Add("partly_sunny ", ":partly_sunny:");
                emojis.Add("white_sun_cloud", ":white_sun_cloud:");
                emojis.Add("cloud ", ":cloud:");
                emojis.Add("white_sun_rain_cloud", ":white_sun_rain_cloud:");
                emojis.Add("cloud_rain", ":cloud_rain:");
                emojis.Add("thunder_cloud_rain", ":thunder_cloud_rain:");
                emojis.Add("cloud_lightning", ":cloud_lightning:");
                emojis.Add("cloud_snow", ":cloud_snow:");
                emojis.Add("snowflake ", ":snowflake:");
                emojis.Add("snowman2 ", ":snowman2:");
                emojis.Add("snowman ", ":snowman:");
                emojis.Add("wind_blowing_face", ":wind_blowing_face:");
                emojis.Add("dash", ":dash:");
                emojis.Add("droplet", ":droplet:");
                emojis.Add("sweat_drops", ":sweat_drops:");
                emojis.Add("umbrella ", ":umbrella:");
                emojis.Add("umbrella2 ", ":umbrella2:");
                emojis.Add("ocean", ":ocean:");
                emojis.Add("fog", ":fog:");
                emojis.Add("green_apple", ":green_apple:");
                emojis.Add("apple", ":apple:");
                emojis.Add("pear", ":pear:");
                emojis.Add("tangerine", ":tangerine:");
                emojis.Add("lemon", ":lemon:");
                emojis.Add("banana", ":banana:");
                emojis.Add("watermelon", ":watermelon:");
                emojis.Add("grapes", ":grapes:");
                emojis.Add("strawberry", ":strawberry:");
                emojis.Add("melon", ":melon:");
                emojis.Add("cherries", ":cherries:");
                emojis.Add("peach", ":peach:");
                emojis.Add("pineapple", ":pineapple:");
                emojis.Add("kiwi", ":kiwi:");
                emojis.Add("tomato", ":tomato:");
                emojis.Add("eggplant", ":eggplant:");
                emojis.Add("avocado", ":avocado:");
                emojis.Add("cucumber", ":cucumber:");
                emojis.Add("hot_pepper", ":hot_pepper:");
                emojis.Add("corn", ":corn:");
                emojis.Add("carrot", ":carrot:");
                emojis.Add("potato", ":potato:");
                emojis.Add("sweet_potato", ":sweet_potato:");
                emojis.Add("croissant", ":croissant:");
                emojis.Add("bread", ":bread:");
                emojis.Add("french_bread", ":french_bread:");
                emojis.Add("cheese", ":cheese:");
                emojis.Add("egg", ":egg:");
                emojis.Add("cooking", ":cooking:");
                emojis.Add("pancakes", ":pancakes:");
                emojis.Add("bacon", ":bacon:");
                emojis.Add("poultry_leg", ":poultry_leg:");
                emojis.Add("meat_on_bone", ":meat_on_bone:");
                emojis.Add("hotdog", ":hotdog:");
                emojis.Add("hamburger", ":hamburger:");
                emojis.Add("fries", ":fries:");
                emojis.Add("pizza", ":pizza:");
                emojis.Add("stuffed_flatbread", ":stuffed_flatbread:");
                emojis.Add("taco", ":taco:");
                emojis.Add("burrito", ":burrito:");
                emojis.Add("salad", ":salad:");
                emojis.Add("shallow_pan_of_food", ":shallow_pan_of_food:");
                emojis.Add("spaghetti", ":spaghetti:");
                emojis.Add("ramen", ":ramen:");
                emojis.Add("stew", ":stew:");
                emojis.Add("curry", ":curry:");
                emojis.Add("sushi", ":sushi:");
                emojis.Add("bento", ":bento:");
                emojis.Add("fried_shrimp", ":fried_shrimp:");
                emojis.Add("rice_ball", ":rice_ball:");
                emojis.Add("rice", ":rice:");
                emojis.Add("rice_cracker", ":rice_cracker:");
                emojis.Add("fish_cake", ":fish_cake:");
                emojis.Add("oden", ":oden:");
                emojis.Add("dango", ":dango:");
                emojis.Add("shaved_ice", ":shaved_ice:");
                emojis.Add("ice_cream", ":ice_cream:");
                emojis.Add("icecream", ":icecream:");
                emojis.Add("cake", ":cake:");
                emojis.Add("birthday", ":birthday:");
                emojis.Add("custard", ":custard:");
                emojis.Add("lollipop", ":lollipop:");
                emojis.Add("candy", ":candy:");
                emojis.Add("chocolate_bar", ":chocolate_bar:");
                emojis.Add("popcorn", ":popcorn:");
                emojis.Add("doughnut", ":doughnut:");
                emojis.Add("cookie", ":cookie:");
                emojis.Add("chestnut", ":chestnut:");
                emojis.Add("peanuts", ":peanuts:");
                emojis.Add("honey_pot", ":honey_pot:");
                emojis.Add("milk", ":milk:");
                emojis.Add("baby_bottle", ":baby_bottle:");
                emojis.Add("coffee ", ":coffee:");
                emojis.Add("tea", ":tea:");
                emojis.Add("sake", ":sake:");
                emojis.Add("beer", ":beer:");
                emojis.Add("beers", ":beers:");
                emojis.Add("champagne_glass", ":champagne_glass:");
                emojis.Add("wine_glass", ":wine_glass:");
                emojis.Add("tumbler_glass", ":tumbler_glass:");
                emojis.Add("cocktail", ":cocktail:");
                emojis.Add("tropical_drink", ":tropical_drink:");
                emojis.Add("champagne", ":champagne:");
                emojis.Add("spoon", ":spoon:");
                emojis.Add("fork_and_knife", ":fork_and_knife:");
                emojis.Add("fork_knife_plate", ":fork_knife_plate:");
                emojis.Add("red_car", ":red_car:");
                emojis.Add("taxi", ":taxi:");
                emojis.Add("blue_car", ":blue_car:");
                emojis.Add("bus", ":bus:");
                emojis.Add("trolleybus", ":trolleybus:");
                emojis.Add("race_car", ":race_car:");
                emojis.Add("police_car", ":police_car:");
                emojis.Add("ambulance", ":ambulance:");
                emojis.Add("fire_engine", ":fire_engine:");
                emojis.Add("minibus", ":minibus:");
                emojis.Add("truck", ":truck:");
                emojis.Add("articulated_lorry", ":articulated_lorry:");
                emojis.Add("tractor", ":tractor:");
                emojis.Add("scooter", ":scooter:");
                emojis.Add("bike", ":bike:");
                emojis.Add("motor_scooter", ":motor_scooter:");
                emojis.Add("motorcycle", ":motorcycle:");
                emojis.Add("rotating_light", ":rotating_light:");
                emojis.Add("oncoming_police_car", ":oncoming_police_car:");
                emojis.Add("oncoming_bus", ":oncoming_bus:");
                emojis.Add("oncoming_automobile", ":oncoming_automobile:");
                emojis.Add("oncoming_taxi", ":oncoming_taxi:");
                emojis.Add("aerial_tramway", ":aerial_tramway:");
                emojis.Add("mountain_cableway", ":mountain_cableway:");
                emojis.Add("suspension_railway", ":suspension_railway:");
                emojis.Add("railway_car", ":railway_car:");
                emojis.Add("train", ":train:");
                emojis.Add("mountain_railway", ":mountain_railway:");
                emojis.Add("monorail", ":monorail:");
                emojis.Add("bullettrain_side", ":bullettrain_side:");
                emojis.Add("bullettrain_front", ":bullettrain_front:");
                emojis.Add("light_rail", ":light_rail:");
                emojis.Add("steam_locomotive", ":steam_locomotive:");
                emojis.Add("train2", ":train2:");
                emojis.Add("metro", ":metro:");
                emojis.Add("tram", ":tram:");
                emojis.Add("station", ":station:");
                emojis.Add("airplane ", ":airplane:");
                emojis.Add("airplane_departure", ":airplane_departure:");
                emojis.Add("airplane_arriving", ":airplane_arriving:");
                emojis.Add("airplane_small", ":airplane_small:");
                emojis.Add("seat", ":seat:");
                emojis.Add("satellite_orbital", ":satellite_orbital:");
                emojis.Add("rocket", ":rocket:");
                emojis.Add("helicopter", ":helicopter:");
                emojis.Add("canoe", ":canoe:");
                emojis.Add("sailboat ", ":sailboat:");
                emojis.Add("speedboat", ":speedboat:");
                emojis.Add("motorboat", ":motorboat:");
                emojis.Add("cruise_ship", ":cruise_ship:");
                emojis.Add("ferry", ":ferry:");
                emojis.Add("ship", ":ship:");
                emojis.Add("anchor ", ":anchor:");
                emojis.Add("fuelpump ", ":fuelpump:");
                emojis.Add("construction", ":construction:");
                emojis.Add("vertical_traffic_light", ":vertical_traffic_light:");
                emojis.Add("traffic_light", ":traffic_light:");
                emojis.Add("busstop", ":busstop:");
                emojis.Add("map", ":map:");
                emojis.Add("moyai", ":moyai:");
                emojis.Add("statue_of_liberty", ":statue_of_liberty:");
                emojis.Add("tokyo_tower", ":tokyo_tower:");
                emojis.Add("european_castle", ":european_castle:");
                emojis.Add("japanese_castle", ":japanese_castle:");
                emojis.Add("stadium", ":stadium:");
                emojis.Add("ferris_wheel", ":ferris_wheel:");
                emojis.Add("roller_coaster", ":roller_coaster:");
                emojis.Add("carousel_horse", ":carousel_horse:");
                emojis.Add("fountain ", ":fountain:");
                emojis.Add("beach_umbrella", ":beach_umbrella:");
                emojis.Add("beach", ":beach:");
                emojis.Add("island", ":island:");
                emojis.Add("desert", ":desert:");
                emojis.Add("volcano", ":volcano:");
                emojis.Add("mountain", ":mountain:");
                emojis.Add("mountain_snow", ":mountain_snow:");
                emojis.Add("mount_fuji", ":mount_fuji:");
                emojis.Add("camping", ":camping:");
                emojis.Add("tent ", ":tent:");
                emojis.Add("house", ":house:");
                emojis.Add("house_with_garden", ":house_with_garden:");
                emojis.Add("homes", ":homes:");
                emojis.Add("house_abandoned", ":house_abandoned:");
                emojis.Add("construction_site", ":construction_site:");
                emojis.Add("factory", ":factory:");
                emojis.Add("office", ":office:");
                emojis.Add("department_store", ":department_store:");
                emojis.Add("post_office", ":post_office:");
                emojis.Add("european_post_office", ":european_post_office:");
                emojis.Add("hospital", ":hospital:");
                emojis.Add("bank", ":bank:");
                emojis.Add("hotel", ":hotel:");
                emojis.Add("convenience_store", ":convenience_store:");
                emojis.Add("school", ":school:");
                emojis.Add("love_hotel", ":love_hotel:");
                emojis.Add("wedding", ":wedding:");
                emojis.Add("classical_building", ":classical_building:");
                emojis.Add("church ", ":church:");
                emojis.Add("mosque", ":mosque:");
                emojis.Add("synagogue", ":synagogue:");
                emojis.Add("kaaba", ":kaaba:");
                emojis.Add("shinto_shrine", ":shinto_shrine:");
                emojis.Add("railway_track", ":railway_track:");
                emojis.Add("motorway", ":motorway:");
                emojis.Add("japan", ":japan:");
                emojis.Add("rice_scene", ":rice_scene:");
                emojis.Add("park", ":park:");
                emojis.Add("sunrise", ":sunrise:");
                emojis.Add("sunrise_over_mountains", ":sunrise_over_mountains:");
                emojis.Add("stars", ":stars:");
                emojis.Add("sparkler", ":sparkler:");
                emojis.Add("fireworks", ":fireworks:");
                emojis.Add("city_sunset", ":city_sunset:");
                emojis.Add("city_dusk", ":city_dusk:");
                emojis.Add("cityscape", ":cityscape:");
                emojis.Add("night_with_stars", ":night_with_stars:");
                emojis.Add("milky_way", ":milky_way:");
                emojis.Add("bridge_at_night", ":bridge_at_night:");
                emojis.Add("foggy", ":foggy:");
                emojis.Add("watch ", ":watch:");
                emojis.Add("iphone", ":iphone:");
                emojis.Add("calling", ":calling:");
                emojis.Add("computer", ":computer:");
                emojis.Add("keyboard ", ":keyboard:");
                emojis.Add("desktop", ":desktop:");
                emojis.Add("printer", ":printer:");
                emojis.Add("mouse_three_button", ":mouse_three_button:");
                emojis.Add("trackball", ":trackball:");
                emojis.Add("joystick", ":joystick:");
                emojis.Add("compression", ":compression:");
                emojis.Add("minidisc", ":minidisc:");
                emojis.Add("floppy_disk", ":floppy_disk:");
                emojis.Add("cd", ":cd:");
                emojis.Add("dvd", ":dvd:");
                emojis.Add("vhs", ":vhs:");
                emojis.Add("camera", ":camera:");
                emojis.Add("camera_with_flash", ":camera_with_flash:");
                emojis.Add("video_camera", ":video_camera:");
                emojis.Add("movie_camera", ":movie_camera:");
                emojis.Add("projector", ":projector:");
                emojis.Add("film_frames", ":film_frames:");
                emojis.Add("telephone_receiver", ":telephone_receiver:");
                emojis.Add("telephone ", ":telephone:");
                emojis.Add("pager", ":pager:");
                emojis.Add("fax", ":fax:");
                emojis.Add("tv", ":tv:");
                emojis.Add("radio", ":radio:");
                emojis.Add("microphone2", ":microphone2:");
                emojis.Add("level_slider", ":level_slider:");
                emojis.Add("control_knobs", ":control_knobs:");
                emojis.Add("stopwatch", ":stopwatch:");
                emojis.Add("timer", ":timer:");
                emojis.Add("alarm_clock", ":alarm_clock:");
                emojis.Add("clock", ":clock:");
                emojis.Add("hourglass ", ":hourglass:");
                emojis.Add("hourglass_flowing_sand", ":hourglass_flowing_sand:");
                emojis.Add("satellite", ":satellite:");
                emojis.Add("battery", ":battery:");
                emojis.Add("electric_plug", ":electric_plug:");
                emojis.Add("bulb", ":bulb:");
                emojis.Add("flashlight", ":flashlight:");
                emojis.Add("candle", ":candle:");
                emojis.Add("wastebasket", ":wastebasket:");
                emojis.Add("oil", ":oil:");
                emojis.Add("money_with_wings", ":money_with_wings:");
                emojis.Add("dollar", ":dollar:");
                emojis.Add("yen", ":yen:");
                emojis.Add("euro", ":euro:");
                emojis.Add("pound", ":pound:");
                emojis.Add("moneybag", ":moneybag:");
                emojis.Add("credit_card", ":credit_card:");
                emojis.Add("gem", ":gem:");
                emojis.Add("scales ", ":scales:");
                emojis.Add("wrench", ":wrench:");
                emojis.Add("hammer", ":hammer:");
                emojis.Add("hammer_pick", ":hammer_pick:");
                emojis.Add("tools", ":tools:");
                emojis.Add("pick", ":pick:");
                emojis.Add("nut_and_bolt", ":nut_and_bolt:");
                emojis.Add("gear ", ":gear:");
                emojis.Add("chains", ":chains:");
                emojis.Add("gun", ":gun:");
                emojis.Add("bomb", ":bomb:");
                emojis.Add("knife", ":knife:");
                emojis.Add("dagger", ":dagger:");
                emojis.Add("crossed_swords ", ":crossed_swords:");
                emojis.Add("shield", ":shield:");
                emojis.Add("smoking", ":smoking:");
                emojis.Add("coffin ", ":coffin:");
                emojis.Add("urn", ":urn:");
                emojis.Add("amphora", ":amphora:");
                emojis.Add("crystal_ball", ":crystal_ball:");
                emojis.Add("prayer_beads", ":prayer_beads:");
                emojis.Add("barber", ":barber:");
                emojis.Add("alembic", ":alembic:");
                emojis.Add("telescope", ":telescope:");
                emojis.Add("microscope", ":microscope:");
                emojis.Add("hole", ":hole:");
                emojis.Add("heart", ":heart:");
                emojis.Add("yellow_heart", ":yellow_heart:");
                emojis.Add("green_heart", ":green_heart:");
                emojis.Add("blue_heart", ":blue_heart:");
                emojis.Add("purple_heart", ":purple_heart:");
                emojis.Add("black_heart", ":black_heart:");
                emojis.Add("broken_heart", ":broken_heart:");
                emojis.Add("heart_exclamation ", ":heart_exclamation:");
                emojis.Add("two_hearts", ":two_hearts:");
                emojis.Add("revolving_hearts", ":revolving_hearts:");
                emojis.Add("heartbeat", ":heartbeat:");
                emojis.Add("heartpulse", ":heartpulse:");
                emojis.Add("sparkling_heart", ":sparkling_heart:");
                emojis.Add("cupid", ":cupid:");
                emojis.Add("gift_heart", ":gift_heart:");
                emojis.Add("heart_decoration", ":heart_decoration:");
                emojis.Add("peace ", ":peace:");
                emojis.Add("cross ", ":cross:");
                emojis.Add("star_and_crescent ", ":star_and_crescent:");
                emojis.Add("om_symbol", ":om_symbol:");
                emojis.Add("wheel_of_dharma ", ":wheel_of_dharma:");
                emojis.Add("star_of_david ", ":star_of_david:");
                emojis.Add("six_pointed_star", ":six_pointed_star:");
                emojis.Add("menorah", ":menorah:");
                emojis.Add("yin_yang", ":yin_yang:");
                emojis.Add("orthodox_cross ", ":orthodox_cross:");
                emojis.Add("place_of_worship", ":place_of_worship:");
                emojis.Add("ophiuchus", ":ophiuchus:");
                emojis.Add("aries", ":aries:");
                emojis.Add("taurus", ":taurus:");
                emojis.Add("gemini", ":gemini:");
                emojis.Add("cancer", ":cancer:");
                emojis.Add("leo", ":leo:");
                emojis.Add("virgo", ":virgo:");
                emojis.Add("libra", ":libra:");
                emojis.Add("scorpius", ":scorpius:");
                emojis.Add("sagittarius", ":sagittarius:");
                emojis.Add("capricorn", ":capricorn:");
                emojis.Add("aquarius", ":aquarius:");
                emojis.Add("pisces", ":pisces:");
                emojis.Add("id", ":id:");
                emojis.Add("atom", ":atom:");
                emojis.Add("accept", ":accept:");
                emojis.Add("radioactive ", ":radioactive:");
                emojis.Add("biohazard ", ":biohazard:");
                emojis.Add("mobile_phone_off", ":mobile_phone_off:");
                emojis.Add("vibration_mode", ":vibration_mode:");
                emojis.Add("u6709", ":u6709:");
                emojis.Add("u7121 ", ":u7121:");
                emojis.Add("u7533", ":u7533:");
                emojis.Add("u55b6", ":u55b6:");
                emojis.Add("u6708 ", ":u6708:");
                emojis.Add("eight_pointed_black_star ", ":eight_pointed_black_star:");
                emojis.Add("vs", ":vs:");
                emojis.Add("white_flower", ":white_flower:");
                emojis.Add("ideograph_advantage", ":ideograph_advantage:");
                emojis.Add("secret ", ":secret:");
                emojis.Add("congratulations ", ":congratulations:");
                emojis.Add("u5408", ":u5408:");
                emojis.Add("u6e80", ":u6e80:");
                emojis.Add("u5272", ":u5272:");
                emojis.Add("u7981", ":u7981:");
                emojis.Add("a", ":a:");
                emojis.Add("b", ":b:");
                emojis.Add("ab", ":ab:");
                emojis.Add("cl", ":cl:");
                emojis.Add("o2 ", ":o2:");
                emojis.Add("sos", ":sos:");
                emojis.Add("x", ":x:");
                emojis.Add("o ", ":o:");
                emojis.Add("octagonal_sign", ":octagonal_sign:");
                emojis.Add("no_entry ", ":no_entry:");
                emojis.Add("name_badge", ":name_badge:");
                emojis.Add("no_entry_sign", ":no_entry_sign:");
                emojis.Add("100", ":100:");
                emojis.Add("anger", ":anger:");
                emojis.Add("hotsprings ", ":hotsprings:");
                emojis.Add("no_pedestrians", ":no_pedestrians:");
                emojis.Add("do_not_litter", ":do_not_litter:");
                emojis.Add("no_bicycles", ":no_bicycles:");
                emojis.Add("non_potable_water", ":non_potable_water:");
                emojis.Add("underage", ":underage:");
                emojis.Add("no_mobile_phones", ":no_mobile_phones:");
                emojis.Add("no_smoking", ":no_smoking:");
                emojis.Add("exclamation ", ":exclamation:");
                emojis.Add("grey_exclamation", ":grey_exclamation:");
                emojis.Add("question", ":question:");
                emojis.Add("grey_question", ":grey_question:");
                emojis.Add("bangbang ", ":bangbang:");
                emojis.Add("interrobang ", ":interrobang:");
                emojis.Add("low_brightness", ":low_brightness:");
                emojis.Add("high_brightness", ":high_brightness:");
                emojis.Add("part_alternation_mark ", ":part_alternation_mark:");
                emojis.Add("warning ", ":warning:");
                emojis.Add("children_crossing", ":children_crossing:");
                emojis.Add("trident", ":trident:");
                emojis.Add("fleur_de_lis ", ":fleur_de_lis:");
                emojis.Add("beginner", ":beginner:");
                emojis.Add("recycle ", ":recycle:");
                emojis.Add("white_check_mark", ":white_check_mark:");
                emojis.Add("u6307 ", ":u6307:");
                emojis.Add("chart", ":chart:");
                emojis.Add("sparkle ", ":sparkle:");
                emojis.Add("eight_spoked_asterisk ", ":eight_spoked_asterisk:");
                emojis.Add("negative_squared_cross_mark", ":negative_squared_cross_mark:");
                emojis.Add("globe_with_meridians", ":globe_with_meridians:");
                emojis.Add("diamond_shape_with_a_dot_inside", ":diamond_shape_with_a_dot_inside:");
                emojis.Add("m ", ":m:");
                emojis.Add("cyclone", ":cyclone:");
                emojis.Add("zzz", ":zzz:");
                emojis.Add("atm", ":atm:");
                emojis.Add("wc", ":wc:");
                emojis.Add("wheelchair ", ":wheelchair:");
                emojis.Add("parking ", ":parking:");
                emojis.Add("u7a7a", ":u7a7a:");
                emojis.Add("sa ", ":sa:");
                emojis.Add("passport_control", ":passport_control:");
                emojis.Add("customs", ":customs:");
                emojis.Add("baggage_claim", ":baggage_claim:");
                emojis.Add("left_luggage", ":left_luggage:");
                emojis.Add("mens", ":mens:");
                emojis.Add("womens", ":womens:");
                emojis.Add("baby_symbol", ":baby_symbol:");
                emojis.Add("restroom", ":restroom:");
                emojis.Add("put_litter_in_its_place", ":put_litter_in_its_place:");
                emojis.Add("cinema", ":cinema:");
                emojis.Add("signal_strength", ":signal_strength:");
                emojis.Add("koko", ":koko:");
                emojis.Add("symbols", ":symbols:");
                emojis.Add("information_source ", ":information_source:");
                emojis.Add("abc", ":abc:");
                emojis.Add("abcd", ":abcd:");
                emojis.Add("capital_abcd", ":capital_abcd:");
                emojis.Add("ng", ":ng:");
                emojis.Add("ok", ":ok:");
                emojis.Add("up", ":up:");
                emojis.Add("cool", ":cool:");
                emojis.Add("new", ":new:");
                emojis.Add("free", ":free:");
                emojis.Add("keycap_ten", ":keycap_ten:");
                emojis.Add("1234", ":1234:");
                emojis.Add("hash", "#");
                emojis.Add("eject ", ":eject:");
                emojis.Add("arrow_forward ", ":arrow_forward:");
                emojis.Add("pause_button", ":pause_button:");
                emojis.Add("play_pause", ":play_pause:");
                emojis.Add("stop_button", ":stop_button:");
                emojis.Add("record_button", ":record_button:");
                emojis.Add("track_next", ":track_next:");
                emojis.Add("track_previous", ":track_previous:");
                emojis.Add("fast_forward", ":fast_forward:");
                emojis.Add("rewind", ":rewind:");
                emojis.Add("arrow_double_up", ":arrow_double_up:");
                emojis.Add("arrow_double_down", ":arrow_double_down:");
                emojis.Add("arrow_backward ", ":arrow_backward:");
                emojis.Add("arrow_up_small", ":arrow_up_small:");
                emojis.Add("arrow_down_small", ":arrow_down_small:");
                emojis.Add("arrow_right ", ":arrow_right:");
                emojis.Add("arrow_left ", ":arrow_left:");
                emojis.Add("arrow_up ", ":arrow_up:");
                emojis.Add("arrow_down ", ":arrow_down:");
                emojis.Add("arrow_upper_right ", ":arrow_upper_right:");
                emojis.Add("arrow_lower_right ", ":arrow_lower_right:");
                emojis.Add("arrow_lower_left ", ":arrow_lower_left:");
                emojis.Add("arrow_upper_left ", ":arrow_upper_left:");
                emojis.Add("arrow_up_down ", ":arrow_up_down:");
                emojis.Add("left_right_arrow ", ":left_right_arrow:");
                emojis.Add("arrow_right_hook ", ":arrow_right_hook:");
                emojis.Add("leftwards_arrow_with_hook ", ":leftwards_arrow_with_hook:");
                emojis.Add("arrow_heading_up ", ":arrow_heading_up:");
                emojis.Add("arrow_heading_down ", ":arrow_heading_down:");
                emojis.Add("twisted_rightwards_arrows", ":twisted_rightwards_arrows:");
                emojis.Add("repeat", ":repeat:");
                emojis.Add("repeat_one", ":repeat_one:");
                emojis.Add("arrows_counterclockwise", ":arrows_counterclockwise:");
                emojis.Add("arrows_clockwise", ":arrows_clockwise:");
                emojis.Add("musical_note", ":musical_note:");
                emojis.Add("notes", ":notes:");
                emojis.Add("heavy_plus_sign", ":heavy_plus_sign:");
                emojis.Add("heavy_minus_sign", ":heavy_minus_sign:");
                emojis.Add("heavy_division_sign", ":heavy_division_sign:");
                emojis.Add("heavy_multiplication_x", ":heavy_multiplication_x:");
                emojis.Add("wavy_dash ", ":wavy_dash:");
                emojis.Add("curly_loop", ":curly_loop:");
                emojis.Add("loop", ":loop:");
                emojis.Add("end", ":end:");
                emojis.Add("back", ":back:");
                emojis.Add("top", ":top:");
                emojis.Add("soon", ":soon:");
                emojis.Add("heavy_check_mark ", ":heavy_check_mark:");
                emojis.Add("ballot_box_with_check ", ":ballot_box_with_check:");
                emojis.Add("radio_button", ":radio_button:");
                emojis.Add("white_circle", ":white_circle:");

                //self made
                emojis.Add("hello", ":wave:");
                emojis.Add("hi", ":wave:");
                emojis.Add("heyo", ":wave:");
                emojis.Add("hoi", ":wave:");
                emojis.Add("lol", ":laughing:");
                emojis.Add("haha", ":laughing:");
                emojis.Add("hehe", ":laughing:");
                emojis.Add("harhar", ":laughing:");
                emojis.Add("lmao", ":laughing:");
                emojis.Add("lmfao", ":laughing:");
                emojis.Add("dick", ":eggplant:");
                emojis.Add("pp", ":eggplant:");
                emojis.Add("penis", ":eggplant:");
                emojis.Add("peepee", ":eggplant:");
                emojis.Add("dildo", ":eggplant:");
                emojis.Add("didlo", ":eggplant:");
                emojis.Add("gay", ":gay_pride_flag:");
                emojis.Add("gayy", ":gay_pride_flag:");
                emojis.Add("gai", ":gay_pride_flag:");
                emojis.Add("homo", ":gay_pride_flag:");
                emojis.Add("homosexual", ":gay_pride_flag:");
                emojis.Add("butt", ":peach:");
                emojis.Add("ass", ":peach:");
                emojis.Add("azz", ":peach:");
                emojis.Add("asshole", ":peach:");
                emojis.Add("butthole", ":peach:");
                emojis.Add("anus", ":peach:");
                emojis.Add("thinkin", ":thinking:");
                emojis.Add("contemplating", ":thinking:");
                emojis.Add("hmm", ":thinking:");
                emojis.Add("hm", ":thinking:");
                emojis.Add("hmmm", ":thinking:");
                emojis.Add("uh", ":thinking:");
                emojis.Add("uhh", ":thinking:");
                emojis.Add("uhhh", ":thinking:");
                emojis.Add("thonking", ":thinking:");
                emojis.Add("sex", ":peach::eggplant:");
                emojis.Add("hump", ":peach::eggplant:");
                emojis.Add("do", ":peach::eggplant:");
                emojis.Add("fuck", ":peach::eggplant:");
                emojis.Add("rape", ":peach::eggplant:");
                emojis.Add("raped", ":peach::eggplant:");
                emojis.Add("raping", ":peach::eggplant:");
                emojis.Add("fucks", ":persevere:");
                emojis.Add("fucked", ":persevere:");
                emojis.Add("moan", ":persevere:");
                emojis.Add("moaned", ":persevere:");
                emojis.Add("moaning", ":persevere:");
                emojis.Add("uuh", ":persevere:");
                // ok now unorganized
                emojis.Add("i", ":eye:");
                emojis.Add("i'm", ":eye:");
                emojis.Add("im", ":eye:");
                emojis.Add("ill", ":eye:");
                emojis.Add("i'll", ":eye:");
                emojis.Add("color", ":rainbow:");
                emojis.Add("take", ":right_facing_fist:");
                emojis.Add("took", ":right_facing_fist:");
                emojis.Add("taken", ":right_facing_fist:");
                emojis.Add("grab", ":right_facing_fist:");
                emojis.Add("grabbing", ":right_facing_fist:");
                emojis.Add("grabbed", ":right_facing_fist:");
                emojis.Add("just", ":clock1:");
                emojis.Add("be", ":bee:");
                emojis.Add("and", ":clap:");
                emojis.Add("in", ":door::runner:");
                emojis.Add("that", ":point_up:");
                emojis.Add("it", ":point_up:");
                emojis.Add("for", ":four:");
                emojis.Add("not", ":no_entry_sign:");
                emojis.Add("on", ":flashlight:");
                emojis.Add("he", ":man:");
                emojis.Add("she", ":woman:");
                emojis.Add("you", ":punch:");
                emojis.Add("at", ":chart_with_downwards_trend:");
            }
            catch (Exception)
            {
                KeyValuePair<string, string> kv = emojis.ElementAt(emojis.Count - 1);
                UnhookWindowsHookEx(_keyboardHookID);
                throw new Exception(String.Format("Duplicate found! Last add:\nK: {0}\nV: {1}",
                    kv.Key, kv.Value));
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public void SetBorderCurve(int radius)
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;

            public POINT()
            {/* do nothing */}

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private const int WH_KEYBOARD_LL = 13;
        private LowLevelKeyboardProc _Kproc;
        private static IntPtr _keyboardHookID = IntPtr.Zero;
        public enum WindowsMessages
        {
            WM_ACTIVATE = 0x6,
            WM_ACTIVATEAPP = 0x1C,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37F,
            WM_APP = 0x8000,
            WM_ASKCBFORMATNAME = 0x30C,
            WM_CANCELJOURNAL = 0x4B,
            WM_CANCELMODE = 0x1F,
            WM_CAPTURECHANGED = 0x215,
            WM_CHANGECBCHAIN = 0x30D,
            WM_CHAR = 0x102,
            WM_CHARTOITEM = 0x2F,
            WM_CHILDACTIVATE = 0x22,
            WM_CLEAR = 0x303,
            WM_CLOSE = 0x10,
            WM_COMMAND = 0x111,
            WM_COMPACTING = 0x41,
            WM_COMPAREITEM = 0x39,
            WM_CONTEXTMENU = 0x7B,
            WM_COPY = 0x301,
            WM_COPYDATA = 0x4A,
            WM_CREATE = 0x1,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_CUT = 0x300,
            WM_DEADCHAR = 0x103,
            WM_DELETEITEM = 0x2D,
            WM_DESTROY = 0x2,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DEVICECHANGE = 0x219,
            WM_DEVMODECHANGE = 0x1B,
            WM_DISPLAYCHANGE = 0x7E,
            WM_DRAWCLIPBOARD = 0x308,
            WM_DRAWITEM = 0x2B,
            WM_DROPFILES = 0x233,
            WM_ENABLE = 0xA,
            WM_ENDSESSION = 0x16,
            WM_ENTERIDLE = 0x121,
            WM_ENTERMENULOOP = 0x211,
            WM_ENTERSIZEMOVE = 0x231,
            WM_ERASEBKGND = 0x14,
            WM_EXITMENULOOP = 0x212,
            WM_EXITSIZEMOVE = 0x232,
            WM_FONTCHANGE = 0x1D,
            WM_GETDLGCODE = 0x87,
            WM_GETFONT = 0x31,
            WM_GETHOTKEY = 0x33,
            WM_GETICON = 0x7F,
            WM_GETMINMAXINFO = 0x24,
            WM_GETOBJECT = 0x3D,
            WM_GETTEXT = 0xD,
            WM_GETTEXTLENGTH = 0xE,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35F,
            WM_HELP = 0x53,
            WM_HOTKEY = 0x312,
            WM_HSCROLL = 0x114,
            WM_HSCROLLCLIPBOARD = 0x30E,
            WM_ICONERASEBKGND = 0x27,
            WM_IME_CHAR = 0x286,
            WM_IME_COMPOSITION = 0x10F,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_CONTROL = 0x283,
            WM_IME_ENDCOMPOSITION = 0x10E,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYLAST = 0x10F,
            WM_IME_KEYUP = 0x291,
            WM_IME_NOTIFY = 0x282,
            WM_IME_REQUEST = 0x288,
            WM_IME_SELECT = 0x285,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_STARTCOMPOSITION = 0x10D,
            WM_INITDIALOG = 0x110,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_INPUTLANGCHANGE = 0x51,
            WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_KEYDOWN = 0x100,
            WM_KEYFIRST = 0x100,
            WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,
            WM_KILLFOCUS = 0x8,
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MDIACTIVATE = 0x222,
            WM_MDICASCADE = 0x227,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIGETACTIVE = 0x229,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDINEXT = 0x224,
            WM_MDIREFRESHMENU = 0x234,
            WM_MDIRESTORE = 0x223,
            WM_MDISETMENU = 0x230,
            WM_MDITILE = 0x226,
            WM_MEASUREITEM = 0x2C,
            WM_MENUCHAR = 0x120,
            WM_MENUCOMMAND = 0x126,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_MENURBUTTONUP = 0x122,
            WM_MENUSELECT = 0x11F,
            WM_MOUSEACTIVATE = 0x21,
            WM_MOUSEFIRST = 0x200,
            WM_MOUSEHOVER = 0x2A1,
            WM_MOUSELAST = 0x20A,
            WM_MOUSELEAVE = 0x2A3,
            WM_MOUSEMOVE = 0x200,
            WM_MOUSEWHEEL = 0x20A,
            WM_MOVE = 0x3,
            WM_MOVING = 0x216,
            WM_NCACTIVATE = 0x86,
            WM_NCCALCSIZE = 0x83,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCHITTEST = 0x84,
            WM_NCLBUTTONDBLCLK = 0xA3,
            WM_NCLBUTTONDOWN = 0xA1,
            WM_NCLBUTTONUP = 0xA2,
            WM_NCMBUTTONDBLCLK = 0xA9,
            WM_NCMBUTTONDOWN = 0xA7,
            WM_NCMBUTTONUP = 0xA8,
            WM_NCMOUSEHOVER = 0x2A0,
            WM_NCMOUSELEAVE = 0x2A2,
            WM_NCMOUSEMOVE = 0xA0,
            WM_NCPAINT = 0x85,
            WM_NCRBUTTONDBLCLK = 0xA6,
            WM_NCRBUTTONDOWN = 0xA4,
            WM_NCRBUTTONUP = 0xA5,
            WM_NEXTDLGCTL = 0x28,
            WM_NEXTMENU = 0x213,
            WM_NOTIFY = 0x4E,
            WM_NOTIFYFORMAT = 0x55,
            WM_NULL = 0x0,
            WM_PAINT = 0xF,
            WM_PAINTCLIPBOARD = 0x309,
            WM_PAINTICON = 0x26,
            WM_PALETTECHANGED = 0x311,
            WM_PALETTEISCHANGING = 0x310,
            WM_PARENTNOTIFY = 0x210,
            WM_PASTE = 0x302,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38F,
            WM_POWER = 0x48,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_QUERYDRAGICON = 0x37,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYNEWPALETTE = 0x30F,
            WM_QUERYOPEN = 0x13,
            WM_QUEUESYNC = 0x23,
            WM_QUIT = 0x12,
            WM_RBUTTONDBLCLK = 0x206,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RENDERALLFORMATS = 0x306,
            WM_RENDERFORMAT = 0x305,
            WM_SETCURSOR = 0x20,
            WM_SETFOCUS = 0x7,
            WM_SETFONT = 0x30,
            WM_SETHOTKEY = 0x32,
            WM_SETICON = 0x80,
            WM_SETREDRAW = 0xB,
            WM_SETTEXT = 0xC,
            WM_SETTINGCHANGE = 0x1A,
            WM_SHOWWINDOW = 0x18,
            WM_SIZE = 0x5,
            WM_SIZECLIPBOARD = 0x30B,
            WM_SIZING = 0x214,
            WM_SPOOLERSTATUS = 0x2A,
            WM_STYLECHANGED = 0x7D,
            WM_STYLECHANGING = 0x7C,
            WM_SYNCPAINT = 0x88,
            WM_SYSCHAR = 0x106,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,
            WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_TCARD = 0x52,
            WM_TIMECHANGE = 0x1E,
            WM_TIMER = 0x113,
            WM_UNDO = 0x304,
            WM_UNINITMENUPOPUP = 0x125,
            WM_USER = 0x400,
            WM_USERCHANGED = 0x54,
            WM_VKEYTOITEM = 0x2E,
            WM_VSCROLL = 0x115,
            WM_VSCROLLCLIPBOARD = 0x30A,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WININICHANGE = 0x1A
        }
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }
        [DllImport("USER32.dll")]
        static extern short GetKeyState(Keys nVirtKey);
        static bool GetKeyDown(Keys k)
        {
            return Convert.ToBoolean(GetKeyState(k) & 0x8000);
        }
        private static IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private delegate IntPtr LowLevelKeyboardProc
            (int nCode, IntPtr wParam, IntPtr lParam);
        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WindowsMessages.WM_KEYDOWN)
            {
                if (!isComputerSending)
                {
                    try
                    {
                        int vkCode = Marshal.ReadInt32(lParam);
                        Keys key = (Keys)vkCode;

                        if (key == Keys.D2 &&
                            (GetKeyDown(Keys.LShiftKey) ||
                            GetKeyDown(Keys.RShiftKey)))
                        {
                            IntPtr fore = GetForegroundWindow();
                            string title = GetWindowTitle(fore);
                            if (title.EndsWith("Discord") && mentionKeys < 1)
                                mentionKeys++;
                            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
                        }

                        if (key == Keys.Enter && mentionKeys > 0)
                        {
                            IntPtr fore = GetForegroundWindow();
                            string title = GetWindowTitle(fore);
                            if (title.EndsWith("Discord"))
                                mentionKeys--;
                            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
                        }

                        if (mentionKeys < 0)
                            mentionKeys = 0;

                        if (key == processhotkey)
                        {
                            IntPtr fore = GetForegroundWindow();
                            string title = GetWindowTitle(fore);
                            if (!title.EndsWith("Discord"))
                                return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
                            if(WindowScanner.ButtonOnWindow(fore,
                                Color.FromArgb(240, 71, 71),
                                Color.FromArgb(114, 137, 218)))
                                return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);

                            string text = GetTextBox();
                            if (text.Equals(lastText))
                            {
                                // most likely empty.
                                return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
                            }
                            if (performance)
                                Thread.Sleep(100);
                            if (bypass)
                            {
                                SendKeys.SendWait("a{Enter}");
                                Thread.Sleep(200);
                                SendKeys.SendWait("{Up}^a");
                            }
                            ProcessText(text, true);
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message + ":" + exc.Source + "\n\n" + exc.StackTrace, "TextMod Internal Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
            }
            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }
        [DllImport("kernel32.dll")]
        static extern int GetProcessId(IntPtr handle);
        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);
        public static string GetWindowTitle(IntPtr hWnd)
        {
            int len = GetWindowTextLength(hWnd);
            if (len == -1) return null;
            StringBuilder sb = new StringBuilder(len + 1);
            GetWindowText(hWnd, sb, len + 1);
            return sb.ToString();
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
            if(ImageGenerator.singleton != null)
                ImageGenerator.singleton.Dispose();
            Program.pfc.Dispose();
            foreach (var memAlloc in fontMemoryToBeDeallocated)
                Marshal.FreeCoTaskMem(memAlloc);
            UnhookWindowsHookEx(_keyboardHookID);
        }
        
        /*bool OstrichSansInstalled()
        {
            using (InstalledFontCollection fonts = new InstalledFontCollection())
            {
                return fonts.Families.Any(f =>
                    f.Name.ToLower().Equals("ostrich sans"));
            }
        }*/
        string GetTextBox()
        {
            try {
                if (performance)
                {
                    SendKeys.SendWait("^a");
                    Thread.Sleep(100);
                    SendKeys.SendWait("^c");
                    Thread.Sleep(100);
                }
                else
                {
                    SendKeys.SendWait("^a");
                    Thread.Sleep(5);
                    SendKeys.SendWait("^c");
                    Thread.Sleep(5);
                    //if(slowpc) { Thread.Sleep(45); }
                }
                return Clipboard.GetText();
            } catch(Exception) {
                //slowpc = true;
                //MessageBox.Show("It took a little too long to fetch the clipboard contents... Now running in SlowPC mode.");
                Thread.Sleep(30);
                return Clipboard.GetText();
            }
        }
        string[] BasicTags(string[] words)
        {
            string[] clone = (string[])words.Clone();
            int i = -1;
            foreach(string word in words)
            {
                i++;
                if (word.ToLower().Equals("[time]"))
                {
                    string now;
                    now = DateTime.Now.ToShortTimeString();
                    clone[i] = now;
                } else if(word.ToLower().Equals("[date]"))
                {
                    string now;
                    now = DateTime.Today.ToLongDateString();
                    clone[i] = now;
                } else if(word.ToLower().Equals("[day]"))
                {
                    string now;
                    now = DateTime.Today.DayOfWeek.ToString();
                    clone[i] = now;
                }
            }
            return clone;
        }
        string[] Methods(string[] words)
        {
            if(words[0].StartsWith("-"))
            {
                return words;
            }
            string[] clone = (string[])words.Clone();
            int i = -1;
            foreach(string word in words)
            {
                i++;
                if(word.StartsWith("math(") && word.Contains(")"))
                {
                    int ob, cb;
                    ob = word.IndexOf('(');
                    cb = word.IndexOf(')');
                    string expression = word.Substring(ob + 1, cb - ob - 1);
                    double result = Evaluate(expression);
                    clone[i] = result.ToString();
                } else if (word.StartsWith(":") && word.EndsWith(":"))
                {
                    string ss = word.Substring(1);
                    string emoji = ss.Remove(ss.LastIndexOf(':'));
                    if (emoji == "" || emoji == null)
                    {return clone;}
                    bool png_exists = File.Exists("textModData\\" + emoji + ".png");
                    bool jpg_exists = File.Exists("textModData\\" + emoji + ".jpg");
                    string path = "textModData\\" + emoji;
                    string ext;

                    if(png_exists)
                    {
                        ext = ".png";
                    } else if(jpg_exists)
                    {
                        ext = ".jpg";
                    } else
                    {return clone;}

                    Image img;
                    img = resizeImage(50, 50, path + ext);
                    if(img == null) { return clone; }
                    SendKeys.SendWait("{Backspace}");
                    Clipboard.SetImage(img);
                    if(performance)
                    {
                        SendKeys.SendWait("^v");
                        Thread.Sleep(300);
                        SendKeys.SendWait("{Enter}");
                    } else
                    {
                        SendKeys.SendWait("^v{Enter}");
                    }
                    return null;
                } else if (word.StartsWith("[") && word.EndsWith("]"))
                {
                    string ss = word.Substring(1);
                    string emoji = ss.Remove(ss.LastIndexOf(']'));
                    if (emoji == "" || emoji == null)
                    {return clone;}
                    bool png_exists = File.Exists("textModData\\" + emoji + ".png");
                    bool jpg_exists = File.Exists("textModData\\" + emoji + ".jpg");
                    bool txtfile = File.Exists("textModData\\text\\" + emoji + ".txt");
                    bool isscript = File.Exists("textModData\\scripts\\" + emoji + ".wine");
                    string path = "textModData\\" + emoji;
                    string ext;

                    if (png_exists)
                    {
                        ext = ".png";
                    }
                    else if (jpg_exists)
                    {
                        ext = ".jpg";
                    }
                    else if (txtfile)
                    {
                        string s;
                        ext = ".txt";
                        path = "textModData\\text\\" + emoji;
                        s = File.ReadAllText(path + ext);
                        if(s.Length > 2000)
                        {
                            IEnumerable<string> strings = s.SplitInParts(2000);
                            foreach(string clip in strings)
                            {
                                Clipboard.SetText(clip);
                                if (performance)
                                {
                                    SendKeys.SendWait("^v");
                                    Thread.Sleep(300);
                                    SendKeys.SendWait("{Enter}");
                                }
                                else
                                {
                                    SendKeys.SendWait("^v{Enter}");
                                }
                                Thread.Sleep(250);
                            }
                            return null;
                        }
                        Clipboard.SetText(s);
                        if (performance)
                        {
                            SendKeys.SendWait("^v");
                            Thread.Sleep(300);
                            SendKeys.SendWait("{Enter}");
                        }
                        else
                        {
                            SendKeys.SendWait("^v{Enter}");
                        }
                        return null;
                    } else if (isscript)
                    {
                        path = @"textModData\scripts\" + emoji;
                        ext = ".wine";

                        Process fileopener = new Process();
                        fileopener.StartInfo.FileName = path+ext;
                        fileopener.Start();

                        fileopener.WaitForExit();
                        if(performance) { Thread.Sleep(100); }
                        if (performance)
                        {
                            SendKeys.SendWait("^v");
                            Thread.Sleep(300);
                            SendKeys.SendWait("{Enter}");
                        }
                        else
                        {
                            SendKeys.SendWait("^v{Enter}");
                        }
                        return null;
                    } else { return clone; }
                    Image img;
                    img = Image.FromFile(path + ext);
                    if(img == null) { return clone; }
                    for(int f = -1; f < word.Length; f++)
                        { SendKeys.SendWait(" "); }

                    Clipboard.SetImage(img);
                    if (performance)
                    {
                        SendKeys.SendWait("^v");
                        Thread.Sleep(300);
                        SendKeys.SendWait("{Enter}");
                    }
                    else
                    {
                        SendKeys.SendWait("^v{Enter}");
                    }
                    return null;
                }
            }
            return clone;
        }
        string SubProcess(string text)
        {
            if(text.ToLower().StartsWith("-furry "))
            {
                string main = text.Substring(7);
                string furry = main.ToFurry();
                return furry;
            }
            if (text.ToLower().StartsWith("-odd "))
            {
                string main = text.Substring(5);
                string odd = main.Oddify(0);
                return odd;
            }
            if (text.ToLower().StartsWith("-odd2 "))
            {
                string main = text.Substring(6);
                string odd = main.Oddify(1);
                return odd;
            }
            if (text.ToLower().StartsWith("-reverse "))
            {
                string main = text.Substring(9);
                string rev = main.Reverse();
                return rev;
            }
            if (text.ToLower().StartsWith("-emojipasta "))
            {
                string main = text.Substring(12);
                string dic = stringProvider.Emojify(main);
                return dic;
            }
            if (text.ToLower().StartsWith("-emoji "))
            {
                string main = text.Substring(7);
                string ri = main.RegionalIndicators("abcdefghijklmnopqrstuvwxyz".ToCharArray());
                return ri;
            }
            if (text.ToLower().StartsWith("-clap "))
            {
                string main = text.Substring(6);
                string clap = main.Clap();
                return clap;
            }
            if (text.ToLower().StartsWith("-spoil "))
            {
                string main = text.Substring(7);
                string spoil = main.Spoiler();
                return spoil;
            }
            if (text.ToLower().StartsWith("-nato "))
            {
                string main = text.Substring(6);
                string nato = main.NATO();
                return nato;
            }
            if (text.ToLower().StartsWith("-superscript "))
            {
                string main = text.Substring(13);
                string s = main.MapChars(alphabet, superscript);
                return s;
            }
            if (text.ToLower().StartsWith("-small "))
            {
                string main = text.Substring(6);
                string s = main.MapChars(alphabet, small);
                return s;
            }
            if (text.ToLower().StartsWith("-numbers "))
            {
                string main = text.Substring(9);
                string s = main.ToNumbers();
                return s;
            }
            if (text.ToLower().StartsWith("-long "))
            {
                string main = text.Substring(6);
                string s = main.Spaced();
                return s;
            }
            if (text.ToLower().StartsWith("-climit "))
            {
                string main = text.Substring(8);
                List<string> _s = main.SplitInParts(1800).ToList();
                foreach(string s in _s)
                {
                    Clipboard.SetText(s);
                    SendKeys.SendWait("{Backspace}^v{Enter}");
                    Thread.Sleep(900);
                }
                return "_unidentified";
            }
            if (text.ToLower().StartsWith("-pyramid "))
            {
                string main = text.Substring(9);
                string s = main.Pyramid();
                return s;
            }
            if (text.ToLower().StartsWith("-scramble "))
            {
                string main = text.Substring(10);
                string s = main.Scramble();
                return s;
            }
            if (text.ToLower().StartsWith("-gamertag "))
            {
                string main = text.Substring(10);
                string s = main.ToUpper().MapChars(alphabet, gtag);
                return s;
            }
            if (text.ToLower().StartsWith("-zalgo "))
            {
                string main = text.Substring(7);
                string s = main.MapAppendChars(alphabet, zalgo);
                return s;
            }
            if (text.ToLower().StartsWith("-sounds "))
            {
                string main = text.Substring(8);
                string s = main.Sounds();
                return s;
            }
            if (text.ToLower().StartsWith("-poll "))
            {
                string poll = text.Substring(6);
                Clipboard.SetText("||  || **" + poll + "**");
                SendKeys.SendWait("{Backspace}^v{Enter}");
                Thread.Sleep(500);
                SendKeys.SendWait("{+}:thumbsup:{Enter}");
                Thread.Sleep(100);
                SendKeys.SendWait("{+}:thumbsdown:{Enter}");
                return "_unidentified";
            }
            if (text.ToLower().StartsWith("-binary "))
            {
                string main = text.Substring(8);
                string s = main.ToBinary();
                return s;
            }
            // IMAGE STUFF // IMAGE STUFF // IMAGE STUFF // IMAGE STUFF
            if (text.ToLower().StartsWith("-image "))
            {
                string main = text.Substring(7);
                Image i = ImageGenerator.TextToImage(main, 12);
                Clipboard.SetImage(i);
                SendKeys.SendWait("{Backspace}^v{Enter}");
                i.Dispose();
                return "_unidentified";
            }
            if (text.ToLower().StartsWith("-comment "))
            {
                string main = text.Substring(9);
                Image i = ImageGenerator.TextToComment(main);
                Clipboard.SetImage(i);
                SendKeys.SendWait("{Backspace}^v{Enter}");
                i.Dispose();
                return "_unidentified";
            }
            if (text.ToLower().StartsWith("-deepfry "))
            {
                string main = text.Substring(9);
                try
                {
                    using (Image image = ImageEffectManager.DeepFry(main))
                    {
                        Clipboard.SetImage(image);
                        SendKeys.SendWait("{Backspace}^v{Enter}");
                        return "_unidentified";
                    }
                } catch(ArgumentException e) {
                    MessageBox.Show(e.StackTrace, "Error! - " + e.Message);
                    return "_unidentified";
                }
            }
            if (text.ToLower().StartsWith("-paste "))
            {
                string main = text.Substring(7);
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        string ext = Path.GetExtension(main);
                        string path = @"textModTempData\downloaded" + ext;
                        wc.DownloadFile(main, path);
                        string[] files = new string[] { path };
                        Clipboard.Clear();
                        Image disp = Image.FromFile(path);
                        Clipboard.SetImage(disp);
                        if(!performance)
                        {
                            SendKeys.SendWait("{Backspace}^v{Enter}");
                        } else
                        {
                            SendKeys.SendWait("{Backspace}^v");
                            Thread.Sleep(100);
                            SendKeys.SendWait("{Enter}");
                        }
                        disp.Dispose();
                        return "_unidentified";
                    }

                } catch (ArgumentException e)
                {
                    MessageBox.Show(e.StackTrace, "Error! - " + e.Message);
                    return "_unidentified";
                }
            }
            if (text.ToLower().StartsWith("-download "))
            {
                string main = text.Substring(10);
                try
                {
                    WebClient wc = new WebClient();
                    string ext = Path.GetExtension(main);

                    saveFileDialog1.InitialDirectory = @"textModData";
                    saveFileDialog1.Title = "Downloading Image...";
                    saveFileDialog1.ShowHelp = false;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.FileName = "Replace Me";
                    saveFileDialog1.CheckPathExists = true;
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.ShowDialog();

                    string path = saveFileDialog1.FileName + ext;
                    wc.DownloadFile(main, path);
                    wc.Dispose();
                    string[] files = new string[] { path };
                    Clipboard.Clear();
                    Image disp = Image.FromFile(path);
                    Clipboard.SetImage(disp);
                    SendKeys.SendWait("{Backspace}");
                    MessageBox.Show("Image downloaded!");
                    return "_unidentified";
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show(e.StackTrace, "Error! - " + e.Message);
                    return "_unidentified";
                }
            }
            if (text.ToLower().StartsWith("-define "))
            {
                try
                {
                    string main = text.Substring(8);
                    string url = main.Replace(" ", "");
                    url = @"https://googledictionaryapi.eu-gb.mybluemix.net/?define=" + url + "&lang=en";
                    string _json = DownloadDef(url);
                    if (_json.Equals("404 Not Found"))
                    {
                        return "Word not found! Make sure you're speeling it rite!!!1 (keep in mind words with spaces will not work!)";
                    }
                    JToken json = GetJObject(_json);
                    JToken meaning = json[0]["meaning"];
                    JToken final = null;

                    // Find variations of words.
                    final = meaning.Value<JToken>("exclamation")
                        ?? meaning.Value<JToken>("adjective")
                        ?? meaning.Value<JToken>("verb")
                        ?? meaning.Value<JToken>("noun")
                        ?? null;
                    if(final == null)
                    {
                        return "Found the word, but didn't find any definitions for it.";
                    }
                    int count = final.Count();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("◄▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬►\n");
                    if(count <= 1) {
                        sb.Append("**Definition of " + main + ":**\n");
                    } else {
                        sb.Append("**Definitions of \"" + main + "\":**\n\n");
                    }
                    int subs = 0;
                    for(int i = 0; i < count; i++)
                    {
                        JToken f = final[i];
                        string def = (string)f["definition"];
                        string example = (string)f["example"];
                        if(string.IsNullOrWhiteSpace(def))
                        {
                            subs++;
                            continue;
                        }
                        if(string.IsNullOrEmpty(example))
                        {
                            sb.Append("**Definition #" + (i-subs) + "**: " + def + "\n\n");
                        } else {
                            sb.Append("**Definition #" + (i-subs) + "**: " + def + "\n**Use Case #" + (i-subs) + "**: " + example + "\n\n");
                        }
                    }
                    // Trim newlines off the end.
                    while(sb[sb.Length-1] == '\n')
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append("\n◄▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬►");
                    return sb.ToString();
                } catch(Exception exc) {
                    MessageBox.Show("Something went wrong while trying to get that definition... \n" + exc.Message + "\n\n" + exc.StackTrace, "Definition Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (text.ToLower().StartsWith("-weird "))
            {
                string main = text.Substring(7);
                string s = main.ToLower().Replace("a", "æ").Replace("e", "ḝ").Replace(".", ":")
                    .Replace(",", "").Replace("!", "1").Replace("qu", "kw").Replace("i", "į")
                    .Replace("o", "ő").Replace("u", "ąǜ").Replace("x", "cks").Replace("p", "Þ")
                    .Replace("d", "ď").Replace("f", "Ӻ").Replace("n", "ͷ").Replace("m", "ᵯ")
                    .Replace("c", "ɔ").Replace("r", "ř").Replace("t", "ͳ").Replace("?", "¿");
                return s;
            }
            if (text.ToLower().StartsWith("-lines "))
            {
                string main = text.Substring(7);
                string s = main.ToLower().Replace(" ", "\n");
                return s;
            }
            if (text.ToLower().StartsWith("-react "))
            {
                string main = text.Substring(7);
                IEnumerable<char> chars = main
                    .ToCharArray().Distinct();
                foreach(char c in chars)
                {
                    string rg;
                    if (c.Equals(' '))
                    {
                        rg = ":blue_circle:";
                    }
                    else if (alphabet.Contains(c))
                    {
                        rg = ":regional_indicator_" + c + ": ";
                    }
                    else if (Extensions.numbers.Contains(c))
                    {
                        int index = Extensions.numbers.ToList().IndexOf(c);
                        string word = Extensions._numbers[index];
                        rg = ":" + word + ":";
                    }
                    else
                    {
                        rg = c.ToString();
                    }
                    SendKeys.SendWait("{+}" + rg + "{Enter}");
                    Thread.Sleep(500);
                }
                return "_unidentified";
            }
            // IMAGE STUFF // IMAGE STUFF // IMAGE STUFF // IMAGE STUFF
            return text;
        }
        string[] Snippets(string[] words)
        {
            for(int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if(word.ToLower().Equals("<embed>"))
                {
                    SendFakeEmbed();
                    return null;
                } else if(word.ToLower().Equals("<support>"))
                {
                    Clipboard.SetText("?tag support");
                    SendKeys.SendWait("^v");
                    return null;
                } else if(word.ToLower().Equals("<docs>"))
                {
                    Clipboard.SetText("!docs ");
                    SendKeys.SendWait("^v");
                    return null;
                } else if(word.ToLower().Equals("<bi>") ||
                    word.ToLower().Equals("<ib>"))
                {
                    Clipboard.SetText("******");
                    SendKeys.SendWait("^v{Left}{Left}{Left}");
                    return null;
                } else if(word.ToLower().Equals("<i>"))
                {
                    Clipboard.SetText("**");
                    SendKeys.SendWait("^v{Left}");
                    return null;
                } else if(word.ToLower().Equals("<b>"))
                {
                    Clipboard.SetText("****");
                    SendKeys.SendWait("^v{Left}{Left}");
                    return null;
                } else if(word.ToLower().Equals("<everyone>"))
                {
                    SendKeys.SendWait("{Backspace}" +
                        "+2{Enter}" +
                        "+2{Down}{Enter}" +
                        "+2{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Down}{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Down}{Down}{Down}{Down}{Enter}" +
                        "+2{Down}{Down}{Down}{Down}{Down}{Down}{Down}{Down}{Enter}");
                    return null;
                } else if(word.ToLower().Equals("<onestar>"))
                {
                    Random rand = new Random();
                    int comments = rand.Next(6) + 2;
                    Clipboard.SetText(comments + " Comments | Was this review helpful to you? **Yes No** *Report Abuse*");
                    SendKeys.SendWait("{Backspace}" +
                        "⭐ ★ ★ ★ ★+{Enter}+{Enter}^v{Up}");
                    return null;
                } else if(word.ToLower().Equals("<twostars>"))
                {
                    Random rand = new Random();
                    int comments = rand.Next(6) + 2;
                    Clipboard.SetText(comments + " Comments | Was this review helpful to you? **Yes No** *Report Abuse*");
                    SendKeys.SendWait("{Backspace}" +
                        "⭐ ⭐ ★ ★ ★+{Enter}+{Enter}^v{Up}");
                    return null;
                } else if(word.ToLower().Equals("<threestars>"))
                {
                    Random rand = new Random();
                    int comments = rand.Next(6) + 2;
                    Clipboard.SetText(comments + " Comments | Was this review helpful to you? **Yes No** *Report Abuse*");
                    SendKeys.SendWait("{Backspace}" +
                        "⭐ ⭐ ⭐ ★ ★+{Enter}+{Enter}^v{Up}");
                    return null;
                } else if(word.ToLower().Equals("<fourstars>"))
                {
                    Random rand = new Random();
                    int comments = rand.Next(8) + 2;
                    Clipboard.SetText(comments + " Comments | Was this review helpful to you? **Yes No** *Report Abuse*");
                    SendKeys.SendWait("{Backspace}" +
                        "⭐ ⭐ ⭐ ⭐ ★+{Enter}+{Enter}^v{Up}");
                    return null;
                } else if(word.ToLower().Equals("<fivestars>"))
                {
                    Random rand = new Random();
                    int comments = rand.Next(15) + 2;
                    Clipboard.SetText(comments + " Comments | Was this review helpful to you? **Yes No** *Report Abuse*");
                    SendKeys.SendWait("{Backspace}" +
                        "⭐ ⭐ ⭐ ⭐ ⭐+{Enter}+{Enter}^v{Up}");
                    return null;
                }
            }
            return words;
        }

        bool isComputerSending = false;
        void ProcessText(string text, bool doMethods)
        {
            lastText = text;
            try
            {
                isComputerSending = true;
                string[] words = text.Split(' ');
                if (performance) { Thread.Sleep(30); }
                words = Snippets(words);
                if (words == null)
                { return; }
                if (performance) { Thread.Sleep(30); }
                words = BasicTags(words);
                if (words == null)
                { return; }
                if (performance) { Thread.Sleep(30); }
                if (doMethods)
                    words = Methods(words);
                if (words == null)
                { return; }
                if (performance) { Thread.Sleep(30); }
                string final = SubProcess(string.Join(" ", words));
                if (final.Equals("_unidentified"))
                { return; }
                if (performance) { Thread.Sleep(30); }
                // END
                if (final == null || string.
                IsNullOrEmpty(final))
                {
                    return;
                }
                if (final.Length >= 2000)
                {
                    IEnumerable<string> msgs;
                    msgs = final.SplitInParts(1999);
                    foreach (string msg in msgs)
                    {
                        if (performance) { Thread.Sleep(30); }
                        Clipboard.SetText(msg);
                        if (performance) { Thread.Sleep(30); }
                        if (performance)
                        {
                            SendKeys.SendWait("^v");
                            Thread.Sleep(300);
                            SendKeys.SendWait("{Enter}");
                        }
                        else
                        {
                            SendKeys.SendWait("^v{Enter}");
                        }
                    }
                    return;
                }
                Clipboard.SetText(final);
                if (performance) { Thread.Sleep(30); }
                if (performance)
                {
                    SendKeys.SendWait("^v");
                    Thread.Sleep(300);
                    SendKeys.SendWait("{Enter}");
                }
                else
                {
                    SendKeys.SendWait("^v{Enter}");
                }
            }
            finally
            {
                new Task(() =>
                {
                    Task.Delay(20);
                    isComputerSending = false;
                }).Start();
            }
        }
        public static double Evaluate(string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        public string GetSubstringByString(string a, string b, string c)
        {
            return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
        }
        public void SendFakeEmbed()
        {
            SendKeys.Flush();
            Clipboard.SetText(("||  ||  **Title**\n" +
                "||  ||\n" +
                "||  ||  **Field**\n" +
                "||  ||  Description").Replace
                ("\n", Environment.NewLine));
            SendKeys.SendWait("^v");
        }

        public Image resizeImage(int newWidth, int newHeight, string stPhotoPath)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent, nPercentW, nPercentH;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
                new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return bmPhoto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"textModData"))
            {
                Directory.CreateDirectory(@"textModData").Attributes |= FileAttributes.Hidden;
            }
            Process.Start("explorer.exe", @"textModData");
        }
        private void activatorChanger_Click(object sender, EventArgs e)
        {
            KeyDialog d = new KeyDialog();
            d.ShowDialog();
            Keys k = d.selected;
            File.WriteAllText("textModTempData\\textmodHotkey.bruh", k.ToString());
            processhotkey = k;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                if (!bubbleshown)
                {
                    bubbleshown = true;
                    notifyIcon.ShowBalloonTip(3500);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string line in Changelog) {
                sb.Append("\n" + line);
            }
            sb = sb.Remove(0, 1);
            InfoForm iff = new InfoForm(sb.ToString(), VERSION);
            iff.ShowDialog();
            iff.Dispose();
        }
        private void bypassCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            bypass = bypassCheckbox.Checked;
        }
        [DllImport("user32.dll", EntryPoint = "BlockInput")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] bool fBlockIt);
        public static void BlockInput(TimeSpan span)
        {
            try
            {
                BlockInput(true);
                Thread.Sleep(span);
            }
            finally
            {
                BlockInput(false);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                return;
            }
            activatorDisplay.Text = "Current Key: " + processhotkey;
        }

        void ShowInfo(string s)
        {
            InfoMsgBox box = new InfoMsgBox(s);
            box.ShowDialog();
            box.Dispose();
        }
        private void TextOperators_Click(object sender, EventArgs e)
        {
            ShowInfo(
                "-furry (fuwwy speak UwU)\n" +
                "-odd (oDd TeXt)\n" +
                "-reverse (Reverses text)\n" +
                "-emoji (Converts text to regional indicators)\n" +
                "-emojipasta (Adds appropriate emojis after words)\n" +
                "-clap (Replaces spaces with claps)\n" +
                "-spoil (Seperately spoils each letter)\n" +
                "-nato (Converts letters to phonetics)\n" +
                "-superscript (Tiny text)\n" +
                "-numbers (Converts letters to their position in the alphabet. Like A=1, B=2, and C=3)\n" +
                "-long (m a k e s  t e x t  l o n g)\n" +
                "-pyramid (Makes a pyramid with your text. This is very spammy!)\n" +
                "-small (Not big, not tiny, just small.)\n" +
                "-weird (just plain weird characters)\n" +
                "-lines (Puts each word on a seperate line).\n" +
                "-scramble (Does exactly what you think it does!)\n" +
                "-gamertag (Makes your text look like it came straight out of 2011!)\n" +
                "-zalgo (Makes your text look glitchy!)\n" +
                "-sounds (Sounds out each letter. It's more confusing than you'd think!)\n" +
                "-binary (lotta 0s and 1s)\n" +
                "-react (React to the message above you with whatever you type out!)");
        }
        private void Templates_Click(object sender, EventArgs e)
        {
            ShowInfo(
                "<embed> Makes a fake embed to fool people.\n" +
                "<onestar> Review whatever you want!\n" +
                "<twostars> Review whatever you want!\n" +
                "<threestars> Review whatever you want!\n" +
                "<fourstars> Review whatever you want!\n" +
                "<fivestars> Review whatever you want!\n" +
                "-poll (Makes a poll. The reactions might not work on slower computers.)\n" +
                "-define (Posts the definition(s) of a word.)");
        }
        private void Scripting_Click(object sender, EventArgs e)
        {
            ShowInfo(
                "Place your .wine scripts inside textModData\\scripts\n\n" +
                "If you want your wine file to be mostly invisible, start the file with [apptype headless].\n" +
                "At the end of your script, make sure to run \"copy <whatever>\". This determines what will be sent in discord!");
        }
        private void ImageCommands_Click(object sender, EventArgs e)
        {
            ShowInfo(
                "-image (Converts your text to an image.)\n" +
                "-comment (Creates a youtube comment with your text.)\n" +
                "-deepfry (Specify a valid discord url and it will be deep fried!)\n" +
                "-paste (Specify a valid discord url and it will be downloaded and uploaded automatically.)\n" +
                "-download (Downloads the image specified. Images are directly downloaded to textModData)");
        }
        private void FreeNitro_Click(object sender, EventArgs e)
        {
            ShowInfo(
                "If you want some custom emoji for free, look no further! Drop your preferred " +
                "emojis into the directory given to you when clicking the \"Emoji Folder\" button!\n" +
                "After you've done this, you can use the emoji by typing their file names like a regular emoji!\n" +
                "Example: If your file is named \"shrek.png\" you can get it by typing :shrek:\n\n" +
                "If you want your image in full size, use [ ] instead of ::!\n\n" +

                "This same thing works with text files (Located in the text folder!)\n" +
                "Example: If you have a text file named \"copypasta.txt\", you can get the contents by typing [copypasta]!");
        }
        private void performanceModeButton_Click(object sender, EventArgs e)
        {
            performanceModeButton.Enabled = false;
            performance = true;
            MessageBox.Show("Performance mode has been enabled. Restart the software to disable it.", "PerformanceMode", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        string DownloadDef(string url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    string s = wc.DownloadString(url);
                    return s;
                }
            } catch(WebException) {
                return "404 Not Found";
            }
        }
        JArray GetJObject(string s)
        {
            return JArray.Parse(s);
        }
        private static void AddFontFromResource(PrivateFontCollection privateFontCollection, string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(typeof(Form1).Assembly, fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
            fontMemoryToBeDeallocated.Add(fontData);
            // Marshal.FreeCoTaskMem(fontData);
        }

        private static byte[] GetFontResourceBytes(Assembly assembly, string fontResourceName)
        {
            //string[] nms = assembly.GetManifestResourceNames();
            //foreach(string name in nms) { MessageBox.Show(name); }
            var resourceStream = assembly.GetManifestResourceStream("TextMod.Resources." + fontResourceName);
            if (resourceStream == null)
                throw new Exception(string.Format("Unable to find font '{0}' in embedded resources.", fontResourceName));
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;
        }
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
