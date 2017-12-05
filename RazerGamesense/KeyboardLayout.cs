using System.Collections.Generic;

namespace RGS
{
	public class KeyboardLayout
	{
		public static List<string> Mx800KeyZones = new List<string>
		{
			"m0",
			"escape",
			"f1",
			"f2",
			"f3",
			"f4",
			"f5",
			"f6",
			"f7",
			"f8",
			"f9",
			"f10",
			"f11",
			"f12",
			"printscreen",
			"scrollock",
			"pause",

			"m1",
			"backquote",
			"keyboard-1",
			"keyboard-2",
			"keyboard-3",
			"keyboard-4",
			"keyboard-5",
			"keyboard-6",
			"keyboard-7",
			"keyboard-8",
			"keyboard-9",
			"keyboard-0",
			"dash",
			"equal",
			"backspace",
			"insert",
			"home",
			"pageup",
			"keypad-num-lock",
			"keypad-divide",
			"keypad-times",
			"keypad-minus",

			"m2",
			"tab",
			"q",
			"w",
			"e",
			"r",
			"t",
			"y",
			"u",
			"i",
			"o",
			"p",
			"left-bracket",
			"right-bracket",
			"backslash",
			"end",
			"pagedown",
			"keypad-7",
			"keypad-8",
			"keypad-9",
			"keypad-plus", // TODO: Correct row?

			"m3",
			"caps",
			"a",
			"s",
			"d",
			"f",
			"g",
			"h",
			"j",
			"k",
			"l",
			"semicolon",
			"quote",
			"keypad-4",
			"keypad-5",
			"keypad-6",
		    //"keypad-plus",

			"m4",
			"l-shift",
			"z",
			"x",
			"c",
			"v",
			"b",
			"n",
			"m",
			"comma",
			"period",
			"slash",
			"r-shift",
			"uparrow",
			"keypad-1",
			"keypad-2",
			"keypad-3",
			"keypad-enter", // TODO: Correct row?

		    "m5",
			"l-ctrl",
			"l-win",
			"l-alt",
			"spacebar",
			"r-alt",
			"ss-key",
			"win-menu",
			"r-ctrl",
			"leftarrow",
			"downarrow",
			"rightarrow",
			"keypad-0",
			"keypad-period",
		    //"keypad-enter"
		};

		public static List<int> HidKeyboardKeys = new List<int>
		{
			// hw = 17
			// 19
			0, // m0
			41, // Escape
			58, // F1
			59,
			60,
			61,
			61,
			62,
			63,
			64,
			65,
			66,
			67,
			68,
			69, // F12
			70, // Print screen
			71, // Scroll lock
			72, // Pause
			0, // SS logo

			0, // m1
			53, 
			30, 
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			45,
			46,
			42,
			73,
			74,
			75,
			83,
			84,
			85,
			86,

			0, // m2
			43,
			21,
			23,
			28,
			20,
			26,
			8,
			24,
			12,
			18,
			19,
			47,
			48,
			49,
			76,
			77,
			78,
			95,
			96,
			97,
			87, // keypad +, might be wrong row

			0, // m3
			57, // caps
			9,
			10,
			15,
			4,
			22,
			7,
			13,
			14,
			11,
			51,
			52,
			40,
			92,
			93,
			94,
			//87, // keypad + alt

			0, // m4
			54,
			29, 
			27,
			6,
			25,
			5,
			17,
			16,
			225, // l shift
			55,
			56,
			229,
			82,
			89,
			90,
			91,
			88, // keypad enter, might be wrong row

			0, // m5
			224, // l ctrl
			227, // lwin
			226, // lalt
			44, // spacebar
			230, // r alt
			231, // rwin
			118, // menu
			228, // R ctrl
			80, // Left Arrow
			81, // Down arrow
			79, // Right arrow
			98, // keyad 0
			99 // keypad period
			//88 // keypad enter alt
		};
	}
}
