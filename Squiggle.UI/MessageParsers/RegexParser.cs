﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace Squiggle.UI.MessageParsers
{
    abstract class RegexParser: IMessageParser
    {
        protected abstract Regex Pattern { get; }

        public bool TryParseText(string message, out MessageParseResult result)
        {
            var match = Pattern.Match(message);
            if (match.Success)
            {
                Inline converted = Convert(match.Value);
                result = new MessageParseResult(converted);
                result.Prefix = message.Substring(0, match.Index);
                int lastIndex = match.Index + match.Length;
                result.Suffix = message.Substring(lastIndex);
            }
            else
                result = null;
            
            return match.Success;
        }

        abstract protected Inline Convert(string text);
    }
}