namespace RGS.Razer
{
	using System;
	using System.Runtime.InteropServices;

	public class Keyboard
	{
		//! Definitions of keys.
		// Order is important and names should match with the ones from the HidKey enum
		public enum RZKEY
		{
			ESC,

			F1,
			F2,
			F3,
			F4,
			F5,
			F6,
			F7,
			F8,
			F9,
			F10,
			F11,
			F12,

			K1,
			K2,
			K3,
			K4,
			K5,
			K6,
			K7,
			K8,
			K9,
			K0,

			A,
			B,
			C,
			D,
			E,
			F,
			G,
			H,
			I,
			J,
			K,
			L,
			M,
			N,
			O,
			P,
			Q,
			R,
			S,
			T,
			U,
			V,
			W,
			X,
			Y,
			Z,

			NUMLOCK,
			NUMPAD0,
			NUMPAD1,
			NUMPAD2,
			NUMPAD3,
			NUMPAD4,
			NUMPAD5,
			NUMPAD6,
			NUMPAD7,
			NUMPAD8,
			NUMPAD9,
			NUMPAD_DIVIDE,
			NUMPAD_MULTIPLY,
			NUMPAD_SUBTRACT,
			NUMPAD_ADD,
			NUMPAD_ENTER,
			NUMPAD_DECIMAL,

			PRINTSCREEN,
			SCROLL,
			PAUSE,
			INSERT,
			HOME,
			PAGEUP,
			DELETE,
			END,
			PAGEDOWN,

			UP,
			LEFT,
			DOWN,
			RIGHT,

			TAB,
			CAPSLOCK,
			BACKSPACE,
			ENTER,
			LCTRL,
			LWIN,
			LALT,
			SPACE,
			RALT,
			//FN, // Not found on HID
			RMENU,
			RCTRL,
			LSHIFT,
			RSHIFT,
			//MACRO1,
			//MACRO2,
			//MACRO3,
			//MACRO4,
			//MACRO5,
			HASHTILDE,
			MINUS,
			EQUAL,
			LEFTBRACE,
			RIGHTBRACE,
			BACKSLASH,
			SEMICOLON,
			APOSTROPHE,
			COMMA,
			DOT,
			SLASH,
			//EUR_1,
			//EUR_2,
			//JPN_1,
			//JPN_2,
			//JPN_3,
			//JPN_4,
			//JPN_5,
			//KOR_1,
			//KOR_2,
			//KOR_3,
			//KOR_4,
			//KOR_5,
			//KOR_6,
			//KOR_7,
			//INVALID,
		}

		//! Definition of LEDs.
		enum RZLED
		{
			RZLED_LOGO = 0x0014                 /*!< Razer logo */
		}

		//! Maximum number of rows in a keyboard.
		public const int MAX_ROW = 6;

		//! Maximum number of columns in a keyboard.
		public const int MAX_COLUMN = 22;

		//! Maximum number of keys.
		const uint MAX_KEYS = MAX_ROW * MAX_COLUMN;

		//! Maximum number of custom effects.
		const uint MAX_CUSTOM_EFFECTS = MAX_KEYS;

		//! Keyboard LED layout.
		uint[][] RZKEY_LAYOUT;

		//! Chroma keyboard effect types
		public enum EFFECT_TYPE
		{
			CHROMA_NONE = 0,            //!< No effect.
			CHROMA_BREATHING,           //!< Breathing effect.
			CHROMA_CUSTOM,              //!< Custom effect.
			CHROMA_REACTIVE,            //!< Reactive effect.
			CHROMA_STATIC,              //!< Static effect.
			CHROMA_SPECTRUMCYCLING,     //!< Spectrum cycling effect.
			CHROMA_WAVE,                //!< Wave effect.
			CHROMA_RESERVED,            //!< TODO.
			CHROMA_CUSTOM_KEY,          //!< Custom effects with keys.
			CHROMA_INVALID              //!< Invalid effect.
		}

		// Chroma keyboard effects
		//! Breathing effect type
		class BREATHING_EFFECT_TYPE
		{
			//! Breathing effects.
			enum Type
			{
				TWO_COLORS = 1, //!< 2 colors
				RANDOM_COLORS, //!< Random colors
				INVALID //!< Invalid type
			}

			uint Color1; //!< First color.
			uint Color2; //!< Second color.
		}

		//! Custom effect using a matrix type.
		[StructLayout(LayoutKind.Sequential, Size = MAX_ROW * MAX_COLUMN), Serializable]
		public struct CUSTOM_EFFECT_TYPE
		{
			/// uint[22*6]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ROW * MAX_COLUMN)]
			public uint[] Color;      //!< Grid layout. 6 rows by 22 columns.
		}

		//! Custom effect with keys.
		class CUSTOM_KEY_EFFECT_TYPE
		{
			uint[][] Color;        //!< Grid layout. 6 rows by 22 columns.
			uint[][] Key;          //!< Keys information. 6 rows by 22 columns. To indidate there is a key effect, OR with 0x01000000. i.e. Key[0][1] = 0x01000000 | Color;
		}

		//! Reactive effect type
		class REACTIVE_EFFECT_TYPE
		{
			//! Duration of the effect.
			enum Duration
			{
				DURATION_NONE = 0,    //!< No duration.
				DURATION_SHORT,     //!< Short duration.
				DURATION_MEDIUM,    //!< Medium duration.
				DURATION_LONG,      //!< Long duration.
				DURATION_INVALID    //!< Invalid duration.
			}

			uint Color;         //!< Color of the effect
		}

		//! Starlight effect.
		class STARLIGHT_EFFECT_TYPE
		{
			//! Starlight effect types.
			enum _Type
			{
				TWO_COLORS = 1, //!< 2 colors.
				RANDOM_COLORS   //!< Random colors
			}

			uint Color1;    //!< First color.
			uint Color2;    //!< Second color.

			//! Duration of the effect.
			enum _Duration
			{
				DURATION_SHORT = 1, //!< Short duration.
				DURATION_MEDIUM,    //!< Medium duration.
				DURATION_LONG       //!< Long duration.
			}

		}

		//! Static effect type
		class STATIC_EFFECT_TYPE
		{
			uint Color;     //!< Color of the effect
		}

		//! Wave effect type
		class WAVE_EFFECT_TYPE
		{
			//! Direction of the wave effect.
			enum Direction
			{
				DIRECTION_NONE = 0,           //!< No direction.
				DIRECTION_LEFT_TO_RIGHT,    //!< Left to right.
				DIRECTION_RIGHT_TO_LEFT,    //!< Right to left.
				DIRECTION_INVALID           //!< Invalid direction.
			}
		}
	}
}
