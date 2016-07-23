using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Nzl.Hook
{
    public static class KeysHash
    {
        static private Hashtable _UsedKeysHash;  //拼音哈希表

        #region static stor.  //拼音表
        static KeysHash()
        {
            _UsedKeysHash = new Hashtable();

            // 摘要:		
            // 从键值提取修饰符的位屏蔽。		
            _UsedKeysHash.Add(-65536, "Modifiers");
            //		
            // 摘要:		
            // 没有按任何键。		
            _UsedKeysHash.Add(0, "None");
            //		
            // 摘要:		
            // 鼠标左按钮。		
            _UsedKeysHash.Add(1, "LButton");
            //		
            // 摘要:		
            // 鼠标右按钮。		
            _UsedKeysHash.Add(2, "RButton");
            //		
            // 摘要:		
            // Cancel 键。		
            _UsedKeysHash.Add(3, "Cancel");
            //		
            // 摘要:		
            // 鼠标中按钮（三个按钮的鼠标）。		
            _UsedKeysHash.Add(4, "MButton");
            //		
            // 摘要:		
            // 第一个 X 鼠标按钮（五个按钮的鼠标）。		
            _UsedKeysHash.Add(5, "XButton1");
            //		
            // 摘要:		
            // 第二个 X 鼠标按钮（五个按钮的鼠标）。		
            _UsedKeysHash.Add(6, "XButton2");
            //		
            // 摘要:		
            // Backspace 键。		
            _UsedKeysHash.Add(8, "Back");
            //		
            // 摘要:		
            // Tab 键。		
            _UsedKeysHash.Add(9, "Tab");
            //		
            // 摘要:		
            // LINEFEED 键。		
            _UsedKeysHash.Add(10, "LineFeed");
            //		
            // 摘要:		
            // Clear 键。		
            _UsedKeysHash.Add(12, "Clear");
            //		
            // 摘要:		
            // Enter 键。		
            _UsedKeysHash.Add(13, "Enter");
            //		
            // 摘要:		
            // Return 键。		
            _UsedKeysHash.Add(13, "Return");

            //		
            // 摘要:		
            // Shift 键。		
            _UsedKeysHash.Add(16, "ShiftKey");
            //		
            // 摘要:		
            // Ctrl 键。		
            _UsedKeysHash.Add(17, "ControlKey");
            //		
            // 摘要:		
            // Alt 键。		
            _UsedKeysHash.Add(18, "Menu");

            //		
            // 摘要:		
            // Pause 键。		
            _UsedKeysHash.Add(19, "Pause");
            //		
            // 摘要:		
            // Caps Lock 键。		
            _UsedKeysHash.Add(20, "CapsLock");
            //		
            // 摘要:		
            // Caps Lock 键。		
            _UsedKeysHash.Add(20, "Capital");
            //		
            // 摘要:		
            // IME Kana 模式键。		
            _UsedKeysHash.Add(21, "KanaMode");
            //		
            // 摘要:		
            // IME Hanguel 模式键。（为了保持兼容性而设置,使用 HangulMode）		
            _UsedKeysHash.Add(21, "HanguelMode");
            //		
            // 摘要:		
            // IME Hangul 模式键。		
            _UsedKeysHash.Add(21, "HangulMode");
            //		
            // 摘要:		
            // IME Junja 模式键。		
            _UsedKeysHash.Add(23, "JunjaMode");
            //		
            // 摘要:		
            // IME 最终模式键。		
            _UsedKeysHash.Add(24, "FinalMode");
            //		
            // 摘要:		
            // IME Kanji 模式键。		
            _UsedKeysHash.Add(25, "KanjiMode");
            //		
            // 摘要:		
            // IME Hanja 模式键。		
            _UsedKeysHash.Add(25, "HanjaMode");
            //		
            // 摘要:		
            // Esc 键。		
            _UsedKeysHash.Add(27, "Escape");
            //		
            // 摘要:		
            // IME 转换键。		
            _UsedKeysHash.Add(28, "IMEConvert");
            //		
            // 摘要:		
            // IME 非转换键。		
            _UsedKeysHash.Add(29, "IMENonconvert");
            //		
            // 摘要:		
            // IME 接受键。已过时，请改用 System.Windows.Forms.Keys.IMEAccept。		
            _UsedKeysHash.Add(30, "IMEAceept");
            //		
            // 摘要:		
            // IME 接受键，替换 System.Windows.Forms.Keys.IMEAceept。		
            _UsedKeysHash.Add(30, "IMEAccept");
            //		
            // 摘要:		
            // IME 模式更改键。		
            _UsedKeysHash.Add(31, "IMEModeChange");
            //		
            // 摘要:		
            // 空格键。		
            _UsedKeysHash.Add(32, "Space");
            //		
            // 摘要:		
            // Page Up 键。		
            _UsedKeysHash.Add(33, "Prior");

            //		
            // 摘要:		
            // Page Up 键。		
            _UsedKeysHash.Add(33, "PageUp");
            //		
            // 摘要:		
            // Page Down 键。		
            _UsedKeysHash.Add(34, "Next");
            //		
            // 摘要:		
            // Page Down 键。		
            _UsedKeysHash.Add(34, "PageDown");
            //		
            // 摘要:		
            // End 键。		
            _UsedKeysHash.Add(35, "End");
            //		
            // 摘要:		
            // Home 键。		
            _UsedKeysHash.Add(36, "Home");
            //		
            // 摘要:		
            // 向左键。		
            _UsedKeysHash.Add(37, "Left");
            //		
            // 摘要:		
            // 向上键。		
            _UsedKeysHash.Add(38, "Up");
            //		
            // 摘要:		
            // 向右键。		
            _UsedKeysHash.Add(39, "Right");
            //		
            // 摘要:		
            // 向下键。		
            _UsedKeysHash.Add(40, "Down");
            //		
            // 摘要:		
            // Select 键。		
            _UsedKeysHash.Add(41, "Select");
            //		
            // 摘要:		
            // Print 键。		
            _UsedKeysHash.Add(42, "Print");
            //		
            // 摘要:		
            // EXECUTE 键。		
            _UsedKeysHash.Add(43, "Execute");
            //		
            // 摘要:		
            // Print Screen 键。		
            _UsedKeysHash.Add(44, "PrintScreen");
            //		
            // 摘要:		
            // Print Screen 键。		
            _UsedKeysHash.Add(44, "Snapshot");
            //		
            // 摘要:		
            // Ins 键。		
            _UsedKeysHash.Add(45, "Insert");
            //		
            // 摘要:		
            // DeL 键。		
            _UsedKeysHash.Add(46, "Delete");
            //		
            // 摘要:		
            // Help 键。		
            _UsedKeysHash.Add(47, "Help");

            //		
            // 摘要:		
            // 0 键。		
            _UsedKeysHash.Add(48, "D0");
            //		
            // 摘要:		
            // 1 键。		
            _UsedKeysHash.Add(49, "D1");
            //		
            // 摘要:		
            // 2 键。		
            _UsedKeysHash.Add(50, "D2");
            //		
            // 摘要:		
            // 3 键。		
            _UsedKeysHash.Add(51, "D3");
            //		
            // 摘要:		
            // 4 键。		
            _UsedKeysHash.Add(52, "D4");
            //		
            // 摘要:		
            // 5 键。		
            _UsedKeysHash.Add(53, "D5");
            //		
            // 摘要:		
            // 6 键。		
            _UsedKeysHash.Add(54, "D6");
            //		
            // 摘要:		
            // 7 键。		
            _UsedKeysHash.Add(55, "D7");
            //		
            // 摘要:		
            // 8 键。		
            _UsedKeysHash.Add(56, "D8");
            //		
            // 摘要:		
            // 9 键。		
            _UsedKeysHash.Add(57, "D9");
            //		
            // 摘要:		
            // A 键。		
            _UsedKeysHash.Add(65, "A");
            //		
            // 摘要:		
            // B 键。		
            _UsedKeysHash.Add(66, "B");
            //		
            // 摘要:		
            // C 键。		
            _UsedKeysHash.Add(67, "C");
            //		
            // 摘要:		
            // D 键。		
            _UsedKeysHash.Add(68, "D");
            //		
            // 摘要:		
            // E 键。		
            _UsedKeysHash.Add(69, "E");
            //		
            // 摘要:		
            // F 键。		
            _UsedKeysHash.Add(70, "F");
            //		
            // 摘要:		
            // G 键。		
            _UsedKeysHash.Add(71, "G");
            //		
            // 摘要:		
            // H 键。		
            _UsedKeysHash.Add(72, "H");
            //		
            // 摘要:		
            // I 键。		
            _UsedKeysHash.Add(73, "I");
            //		
            // 摘要:		
            // J 键。		
            _UsedKeysHash.Add(74, "J");
            //		
            // 摘要:		
            // K 键。		
            _UsedKeysHash.Add(75, "K");
            //		
            // 摘要:		
            // L 键。		
            _UsedKeysHash.Add(76, "L");
            //		
            // 摘要:		
            // M 键。		
            _UsedKeysHash.Add(77, "M");
            //		
            // 摘要:		
            // N 键。		
            _UsedKeysHash.Add(78, "N");
            //		
            // 摘要:		
            // O 键。		
            _UsedKeysHash.Add(79, "O");
            //		
            // 摘要:		
            // P 键。		
            _UsedKeysHash.Add(80, "P");
            //		
            // 摘要:		
            // Q 键。		
            _UsedKeysHash.Add(81, "Q");
            //		
            // 摘要:		
            // R 键。		
            _UsedKeysHash.Add(82, "R");
            //		
            // 摘要:		
            // S 键。		
            _UsedKeysHash.Add(83, "S");
            //		
            // 摘要:		
            // T 键。		
            _UsedKeysHash.Add(84, "T");
            //		
            // 摘要:		
            // U 键。		
            _UsedKeysHash.Add(85, "U");
            //		
            // 摘要:		
            // V 键。		
            _UsedKeysHash.Add(86, "V");
            //		
            // 摘要:		
            // W 键。		
            _UsedKeysHash.Add(87, "W");
            //		
            // 摘要:		
            // X 键。		
            _UsedKeysHash.Add(88, "X");
            //		
            // 摘要:		
            // Y 键。		
            _UsedKeysHash.Add(89, "Y");
            //		
            // 摘要:		
            // Z 键。		
            _UsedKeysHash.Add(90, "Z");

            //		
            // 摘要:		
            // 左 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。		
            _UsedKeysHash.Add(91, "LWin");
            //		
            // 摘要:		
            // 右 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。		
            _UsedKeysHash.Add(92, "RWin");
            //		
            // 摘要:		
            // 应用程序键（Microsoft Natural Keyboard，人体工程学键盘）。		
            _UsedKeysHash.Add(93, "Apps");
            //		
            // 摘要:		
            // 计算机睡眠键。		
            _UsedKeysHash.Add(95, "Sleep");

            //		
            // 摘要:		
            // 数字键盘上的 0 键。		
            _UsedKeysHash.Add(96, "NumPad0");
            //		
            // 摘要:		
            // 数字键盘上的 1 键。		
            _UsedKeysHash.Add(97, "NumPad1");
            //		
            // 摘要:		
            // 数字键盘上的 2 键。		
            _UsedKeysHash.Add(98, "NumPad2");
            //		
            // 摘要:		
            // 数字键盘上的 3 键。		
            _UsedKeysHash.Add(99, "NumPad3");
            //		
            // 摘要:		
            // 数字键盘上的 4 键。		
            _UsedKeysHash.Add(100, "NumPad4");
            //		
            // 摘要:		
            // 数字键盘上的 5 键。		
            _UsedKeysHash.Add(101, "NumPad5");
            //		
            // 摘要:		
            // 数字键盘上的 6 键。		
            _UsedKeysHash.Add(102, "NumPad6");
            //		
            // 摘要:		
            // 数字键盘上的 7 键。		
            _UsedKeysHash.Add(103, "NumPad7");
            //		
            // 摘要:		
            // 数字键盘上的 8 键。		
            _UsedKeysHash.Add(104, "NumPad8");
            //		
            // 摘要:		
            // 数字键盘上的 9 键。		
            _UsedKeysHash.Add(105, "NumPad9");

            //		
            // 摘要:		
            // 乘号键。		
            _UsedKeysHash.Add(106, "Multiply");
            //		
            // 摘要:		
            // 加号键。		
            _UsedKeysHash.Add(107, "Add");
            //		
            // 摘要:		
            // 分隔符键。		
            _UsedKeysHash.Add(108, "Separator");
            //		
            // 摘要:		
            // 减号键。		
            _UsedKeysHash.Add(109, "Subtract");
            //		
            // 摘要:		
            // 句点键。		
            _UsedKeysHash.Add(110, "Decimal");
            //		
            // 摘要:		
            // 除号键。		
            _UsedKeysHash.Add(111, "Divide");

            //		
            // 摘要:		
            // F1 键。		
            _UsedKeysHash.Add(112, "F1");
            //		
            // 摘要:		
            // F2 键。		
            _UsedKeysHash.Add(113, "F2");
            //		
            // 摘要:		
            // F3 键。		
            _UsedKeysHash.Add(114, "F3");
            //		
            // 摘要:		
            // F4 键。		
            _UsedKeysHash.Add(115, "F4");
            //		
            // 摘要:		
            // F5 键。		
            _UsedKeysHash.Add(116, "F5");
            //		
            // 摘要:		
            // F6 键。		
            _UsedKeysHash.Add(117, "F6");
            //		
            // 摘要:		
            // F7 键。		
            _UsedKeysHash.Add(118, "F7");
            //		
            // 摘要:		
            // F8 键。		
            _UsedKeysHash.Add(119, "F8");
            //		
            // 摘要:		
            // F9 键。		
            _UsedKeysHash.Add(120, "F9");
            //		
            // 摘要:		
            // F10 键。		
            _UsedKeysHash.Add(121, "F10");
            //		
            // 摘要:		
            // F11 键。		
            _UsedKeysHash.Add(122, "F11");
            //		
            // 摘要:		
            // F12 键。		
            _UsedKeysHash.Add(123, "F12");

            //		
            // 摘要:		
            // F13 键。		
            _UsedKeysHash.Add(124, "F13");
            //		
            // 摘要:		
            // F14 键。		
            _UsedKeysHash.Add(125, "F14");
            //		
            // 摘要:		
            // F15 键。		
            _UsedKeysHash.Add(126, "F15");
            //		
            // 摘要:		
            // F16 键。		
            _UsedKeysHash.Add(127, "F16");
            //		
            // 摘要:		
            // F17 键。		
            _UsedKeysHash.Add(128, "F17");
            //		
            // 摘要:		
            // F18 键。		
            _UsedKeysHash.Add(129, "F18");
            //		
            // 摘要:		
            // F19 键。		
            _UsedKeysHash.Add(130, "F19");
            //		
            // 摘要:		
            // F20 键。		
            _UsedKeysHash.Add(131, "F20");
            //		
            // 摘要:		
            // F21 键。		
            _UsedKeysHash.Add(132, "F21");
            //		
            // 摘要:		
            // F22 键。		
            _UsedKeysHash.Add(133, "F22");
            //		
            // 摘要:		
            // F23 键。		
            _UsedKeysHash.Add(134, "F23");
            //		
            // 摘要:		
            // F24 键。		
            _UsedKeysHash.Add(135, "F24");
            //		
            // 摘要:		
            // Num Lock 键。		
            _UsedKeysHash.Add(144, "NumLock");
            //		
            // 摘要:		
            // Scroll Lock 键。		
            _UsedKeysHash.Add(145, "Scroll");
            //		
            // 摘要:		
            // 左 Shift 键。		
            _UsedKeysHash.Add(160, "LShiftKey");
            //		
            // 摘要:		
            // 右 Shift 键。		
            _UsedKeysHash.Add(161, "RShiftKey");
            //		
            // 摘要:		
            // 左 Ctrl 键。		
            _UsedKeysHash.Add(162, "LControlKey");
            //		
            // 摘要:		
            // 右 Ctrl 键。		
            _UsedKeysHash.Add(163, "RControlKey");
            //		
            // 摘要:		
            // 左 Alt 键。		
            _UsedKeysHash.Add(164, "LMenu");
            //		
            // 摘要:		
            // 右 Alt 键。		
            _UsedKeysHash.Add(165, "RMenu");
            //		
            // 摘要:		
            // 浏览器后退键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(166, "BrowserBack");
            //		
            // 摘要:		
            // 浏览器前进键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(167, "BrowserForward");
            //		
            // 摘要:		
            // 浏览器刷新键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(168, "BrowserRefresh");
            //		
            // 摘要:		
            // 浏览器停止键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(169, "BrowserStop");
            //		
            // 摘要:		
            // 浏览器搜索键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(170, "BrowserSearch");
            //		
            // 摘要:		
            // 浏览器收藏夹键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(171, "BrowserFavorites");
            //		
            // 摘要:		
            // 浏览器主页键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(172, "BrowserHome");
            //		
            // 摘要:		
            // 静音键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(173, "VolumeMute");
            //		
            // 摘要:		
            // 减小音量键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(174, "VolumeDown");
            //		
            // 摘要:		
            // 增大音量键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(175, "VolumeUp");
            //		
            // 摘要:		
            // 媒体下一曲目键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(176, "MediaNextTrack");
            //		
            // 摘要:		
            // 媒体上一曲目键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(177, "MediaPreviousTrack");
            //		
            // 摘要:		
            // 媒体停止键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(178, "MediaStop");
            //		
            // 摘要:		
            // 媒体播放暂停键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(179, "MediaPlayPause");
            //		
            // 摘要:		
            // 启动邮件键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(180, "LaunchMail");
            //		
            // 摘要:		
            // 选择媒体键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(181, "SelectMedia");
            //		
            // 摘要:		
            // 启动应用程序一键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(182, "LaunchApplication1");
            //		
            // 摘要:		
            // 启动应用程序二键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(183, "LaunchApplication2");
            //		
            // 摘要:		
            // OEM 1 键。		
            _UsedKeysHash.Add(186, "Oem1");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 分号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(186, "OemSemicolon");
            //		
            // 摘要:		
            // 任何国家/地区键盘上的 OEM 加号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(187, "Oemplus");
            //		
            // 摘要:		
            // 任何国家/地区键盘上的 OEM 逗号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(188, "Oemcomma");
            //		
            // 摘要:		
            // 任何国家/地区键盘上的 OEM 减号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(189, "OemMinus");
            //		
            // 摘要:		
            // 任何国家/地区键盘上的 OEM 句点键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(190, "OemPeriod");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 问号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(191, "OemQuestion");
            //		
            // 摘要:		
            // OEM 2 键。		
            _UsedKeysHash.Add(191, "Oem2");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 波形符键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(192, "Oemtilde");
            //		
            // 摘要:		
            // OEM 3 键。		
            _UsedKeysHash.Add(192, "Oem3");
            //		
            // 摘要:		
            // OEM 4 键。		
            _UsedKeysHash.Add(219, "Oem4");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 左括号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(219, "OemOpenBrackets");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 管道键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(220, "OemPipe");
            //		
            // 摘要:		
            // OEM 5 键。		
            _UsedKeysHash.Add(220, "Oem5");
            //		
            // 摘要:		
            // OEM 6 键。		
            _UsedKeysHash.Add(221, "Oem6");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 右括号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(221, "OemCloseBrackets");
            //		
            // 摘要:		
            // OEM 7 键。		
            _UsedKeysHash.Add(222, "Oem7");
            //		
            // 摘要:		
            // 美式标准键盘上的 OEM 单/双引号键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(222, "OemQuotes");
            //		
            // 摘要:		
            // OEM 8 键。		
            _UsedKeysHash.Add(223, "Oem8");
            //		
            // 摘要:		
            // OEM 102 键。		
            _UsedKeysHash.Add(226, "Oem102");
            //		
            // 摘要:		
            // RT 102 键的键盘上的 OEM 尖括号或反斜杠键（Windows 2000 或更高版本）。		
            _UsedKeysHash.Add(226, "	OemBackslash	");
            //		
            // 摘要:		
            // Process Key 键。		
            _UsedKeysHash.Add(229, "ProcessKey");
            //		
            // 摘要:		
            // 用于将 Unicode 字符当作键击传递。Packet 键值是用于非键盘输入法的 32 位虚拟键值的低位字。		
            _UsedKeysHash.Add(231, "Packet");
            //		
            // 摘要:		
            // Attn 键。		
            _UsedKeysHash.Add(246, "Attn");
            //		
            // 摘要:		
            // Crsel 键。		
            _UsedKeysHash.Add(247, "Crsel	");
            //		
            // 摘要:		
            // Exsel 键。		
            _UsedKeysHash.Add(248, "Exsel");
            //		
            // 摘要:		
            // ERASE EOF 键。		
            _UsedKeysHash.Add(249, "EraseEof");
            //		
            // 摘要:		
            // Play 键。		
            _UsedKeysHash.Add(250, "Play");
            //		
            // 摘要:		
            // Zoom 键。		
            _UsedKeysHash.Add(251, "Zoom");
            //		
            // 摘要:		
            // 保留以备将来使用的常数。		
            _UsedKeysHash.Add(252, "NoName");
            //		
            // 摘要:		
            // PA1 键。		
            _UsedKeysHash.Add(253, "Pa1");
            //		
            // 摘要:		
            // Clear 键。		
            _UsedKeysHash.Add(254, "OemClear");
            //		
            // 摘要:		
            // 从键值提取键代码的位屏蔽。		
            _UsedKeysHash.Add(65535, "KeyCode");
            //		
            // 摘要:		
            // Shift 修改键。		
            _UsedKeysHash.Add(65536, "Shift");
            //		
            // 摘要:		
            // Ctrl 修改键。		
            _UsedKeysHash.Add(131072, "Control");
            //		
            // 摘要:		
            // Alt 修改键。		
            _UsedKeysHash.Add(262144, "Alt");
        }
        #endregion

        public static Hashtable Keys
        {
            get
            {
                return _UsedKeysHash;
            }
        }
    }
}