using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApiQF.Interfaces
{
    public interface ILogger
    {
        void Debug(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");

        void Debug(StringBuilder message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");

        void Trace(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");

        void Warn(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");

        void Error(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");

        void Error(Exception ex, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "");
    }
}