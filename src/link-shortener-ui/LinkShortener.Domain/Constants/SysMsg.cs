using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Domain.Constants
{
    public static class SysMsg
    {
        private readonly static ImmutableDictionary<MsgCodes, string> _messages = new Dictionary<MsgCodes, string>()
    {
        { MsgCodes.LS_SUC_001_Success, "Success." },
    }.ToImmutableDictionary();

        public static string GetMsg(MsgCodes key, params object?[] args)
        {
            KeyValuePair<MsgCodes, string> kvp = _messages.FirstOrDefault(x => x.Key == key);
            return string.Format(kvp.Key + ": " + kvp.Value, args);
        }
    }
    public enum MsgCodes
    {
        LS_SUC_001_Success,
    }
}
