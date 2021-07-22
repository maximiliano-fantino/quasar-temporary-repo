using ApiQF.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiQF.Services
{
    public class MessageService
    {
        public int Difference { get; set; } = int.MinValue;
        public int MaxLength { get; set; }

        public MessageService()
        {
        }

        /// <summary>
        /// Function to decode message
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public String GetMessage(List<List<String>> messages)
        {
            if (messages == null || messages.Count == 0 || (messages.Count == 1 && messages[0].Contains(String.Empty)))
                throw new InvalidMessageException("Los mensajes a procesar son invalidos");
            
            FindGap(messages);
            return DecodeMessage(messages);
        }

        private void FindGap(List<List<String>> messages)
        {
            var intersection = messages.Skip(1).Aggregate(new HashSet<String>(messages.First()), (h, e) => { h.IntersectWith(e); return h; });
            intersection.Remove(String.Empty);
            if (intersection.Count == 0)
                throw new NoMatchingMessageException("Los mensajes no estan relacionados entre si");
            var word = intersection.First();

            MaxLength = messages.Max(StrArr => StrArr == null ? 0 : StrArr.Count);
            foreach (var matchWord in intersection)
            {
                int wordPosition = -1;
                foreach (var _message in messages)
                {
                    var currentindexOf = _message.IndexOf(matchWord);
                    if (wordPosition == -1)
                    {
                        wordPosition = currentindexOf;
                        continue;
                    }
                    if (wordPosition == currentindexOf)
                    {
                        continue;
                    }
                    if (Difference == int.MinValue)
                    {
                        Difference = currentindexOf - wordPosition;
                        continue;
                    }
                    if (Difference != (currentindexOf - wordPosition))
                    {
                        throw new GapOutOfRangeException("Se encontraron diferentes desfasajes en el mensaje");
                    }
                }
            }
            Difference = (Difference == int.MinValue) ? 0 : Math.Abs(Difference);
        }

        private String DecodeMessage(List<List<String>> messages)
        {
            var MessageDecoded = new List<string>();
            foreach (var m in messages)
            {
                var messageArr = m;
                if (m.Count == MaxLength)
                    messageArr.RemoveRange(0, Math.Min(Difference, messageArr.Count));

                for (var x = 0; x < messageArr.Count(); x++)
                {
                    if (MessageDecoded.ElementAtOrDefault(x) == null)
                    {
                        MessageDecoded.Add(messageArr[x]);
                        continue;
                    }
                    if (MessageDecoded.ElementAtOrDefault(x) != null && MessageDecoded.ElementAtOrDefault(x).Equals(String.Empty))
                    {
                        MessageDecoded[x] = messageArr[x];
                        continue;
                    }
                    if (messageArr[x].Equals(String.Empty))
                    {
                        continue;
                    }
                    if (!MessageDecoded.ElementAtOrDefault(x).Equals(messageArr[x]))
                    {
                        throw new ImposibleToDecodeMessageException($"Palabras incompatibles en la posicion {x}: {MessageDecoded.ElementAtOrDefault(x)} y {messageArr[x]}");
                    }
                }
            }
            if (MessageDecoded.Contains(String.Empty))
                throw new ImposibleToDecodeMessageException($"El mensaje decodificado esta incompleto ({String.Join(' ', MessageDecoded)})");
            return String.Join(' ', MessageDecoded);
        }
    }
}