using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public enum QuestionScenarioType
    {
        Simple, //просто вызываем следующий вопрос - next:id
        Complex //взависимости от ответа вызываем соответствующий вопрос
    }
}
