﻿using Application.Commands.Messages.PinMessage;
using Application.Models;
using Tests.Common;

namespace Tests.Messages.Commands
{
    public class PinMessageTests : TestBase
    {
        [Fact]
        public async Task Success()
        {
            //Arrange
            int messageId = 1;

            PinMessageRequest request = new()
            {
                MessageId = messageId
            };
            PinMessageRequestHandler handler = new(Context, UserProvider);

            //Act
            Message result = await handler.Handle(request, CancellationToken);

            //Assert
            Assert.True(result.IsPinned);
        }
    }
}