using System.Collections.Generic;

namespace Gerontocracy.Core.Extensions
{
    public static class CommonExtensions
    {
        public static Data.Entities.Board.Post ToTree(this IEnumerable<Data.Entities.Board.Post> elements)
        {
            var message = new Data.Entities.Board.Post();
            var parents = new Stack<Data.Entities.Board.Post>();
            parents.Push(message);
            foreach (var element in elements)
            {
                while (element.Parent != parents.Peek())
                {
                    parents.Pop();
                }
                parents.Peek().Children.Add(element);
                parents.Push(element);
            }
            return message;
        }
    }
}
