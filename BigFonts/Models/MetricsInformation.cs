using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Models
{
    // /src/Microsoft.DotNet.Wpf/src/Shared/MS/Win32/NativeMethodsCLR.cs

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class LOGFONT
    {
        public LOGFONT()
        {
        }
        public LOGFONT(LOGFONT lf)
        {
            if (lf == null)
            {
                throw new ArgumentNullException("lf");
            }

            this.lfHeight = lf.lfHeight;
            this.lfWidth = lf.lfWidth;
            this.lfEscapement = lf.lfEscapement;
            this.lfOrientation = lf.lfOrientation;
            this.lfWeight = lf.lfWeight;
            this.lfItalic = lf.lfItalic;
            this.lfUnderline = lf.lfUnderline;
            this.lfStrikeOut = lf.lfStrikeOut;
            this.lfCharSet = lf.lfCharSet;
            this.lfOutPrecision = lf.lfOutPrecision;
            this.lfClipPrecision = lf.lfClipPrecision;
            this.lfQuality = lf.lfQuality;
            this.lfPitchAndFamily = lf.lfPitchAndFamily;
            this.lfFaceName = lf.lfFaceName;
        }
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class NONCLIENTMETRICS
    {
        public int cbSize = SizeOf();
        public int iBorderWidth = 0;
        public int iScrollWidth = 0;
        public int iScrollHeight = 0;
        public int iCaptionWidth = 0;
        public int iCaptionHeight = 0;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfCaptionFont = null;
        public int iSmCaptionWidth = 0;
        public int iSmCaptionHeight = 0;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfSmCaptionFont = null;
        public int iMenuWidth = 0;
        public int iMenuHeight = 0;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfMenuFont = null;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfStatusFont = null;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfMessageFont = null;

        /// <SecurityNote>
        ///  Critical : Calls critical Marshal.SizeOf
        ///  Safe     : Calls method with trusted input (well known safe type)
        /// </SecurityNote>
        private static int SizeOf()
        {
            return Marshal.SizeOf(typeof(NONCLIENTMETRICS));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ICONMETRICS
    {
        public int cbSize = SizeOf();
        public int iHorzSpacing = 0;
        public int iVertSpacing = 0;
        public int iTitleWrap = 0;
        [MarshalAs(UnmanagedType.Struct)]
        public LOGFONT lfFont = null;

        private static int SizeOf()
        {
            return Marshal.SizeOf(typeof(ICONMETRICS));
        }
    }


}
