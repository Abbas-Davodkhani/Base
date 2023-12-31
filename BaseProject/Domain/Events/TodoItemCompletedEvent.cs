﻿using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TodoItemCompletedEvent : BaseEvent
    {
        public TodoItemCompletedEvent(TodoItem item) { this.Item = item; }

        public TodoItem Item { get; set; }
    }
}
