using ApiQF.Exceptions;
using ApiQF.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ApiQF.Tests
{
    internal class MessageTest
    {
        private MessageService MessageService { get; set; }

        [SetUp]
        public void Setup()
        {
            MessageService = TestHelper.GetMessageService();
        }

        [Test]
        public void GetMessageWithoutMessages()
        {
            Assert.Throws<InvalidMessageException>(
               delegate
               {
                   MessageService.GetMessage(null);
               });
        }

        [Test]
        public void GetMessagetWithoutMatchingMessages()
        {
            var firstMsg = new List<String>() { "", "este", "es", "", "mensaje" };
            var secondMsg = new List<String>() { "", "", "", "un", "" };
            var l = new List<List<String>>() { firstMsg, secondMsg };
            Assert.Throws<NoMatchingMessageException>(
               delegate
               {
                   MessageService.GetMessage(l);
               });
        }

        [Test]
        public void GetMessageWithMultipleGaps()
        {
            var firstMsg = new List<String>() { "", "este", "es", "", "", "mensaje" };
            var secondMsg = new List<String>() { "este", "", "", "mensaje" };
            var thirdMsg = new List<String>() { "este", "", "un", "mensaje" };

            var l = new List<List<String>>() { firstMsg, secondMsg, thirdMsg };
            Assert.Throws<GapOutOfRangeException>(

               delegate
               {
                   MessageService.GetMessage(l);
               });
        }

        [Test]
        public void GetMessageWithMissPositionWords()
        {
            var firstMsg = new List<String>() { "este", "es", "", "" };
            var secondMsg = new List<String>() { "este", "un", "", "mensaje" };
            var thirdMsg = new List<String>() { "este", "", "un", "" };
            var l = new List<List<String>>() { firstMsg, secondMsg, thirdMsg };
            Assert.Throws<ImposibleToDecodeMessageException>(

               delegate
               {
                   MessageService.GetMessage(l);
               });
        }

        [Test]
        public void GetMessageWithoutMatchingWords()
        {
            var firstMsg = new List<String>() { "este", "es", "", "" };
            var secondMsg = new List<String>() { "", "un", "", "mensaje" };
            var thirdMsg = new List<String>() { "este", "", "un", "" };
            var l = new List<List<String>>() { firstMsg, secondMsg, thirdMsg };
            Assert.Throws<NoMatchingMessageException>(

               delegate
               {
                   MessageService.GetMessage(l);
               });
        }

        [Test]
        public void GetMessage()
        {
            var firstMsg = new List<String>() { "este", "es", "", "" };
            var secondMsg = new List<String>() { "este", "", "", "mensaje" };
            var thirdMsg = new List<String>() { "este", "", "un", "" };
            var expectedMsg = "este es un mensaje";
            var l = new List<List<String>>() { firstMsg, secondMsg, thirdMsg };
            Assert.AreEqual(expectedMsg, MessageService.GetMessage(l));
        }
    }
}