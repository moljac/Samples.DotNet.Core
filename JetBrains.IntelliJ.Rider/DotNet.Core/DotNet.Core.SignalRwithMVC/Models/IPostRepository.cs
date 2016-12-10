
namespace DotNet.Core.SignalR.Models
{
    public interface IPostRepository
    {
        System.Collections.Generic.List<Post> GetAll();
        Post GetPost(int id);
        void AddPost(Post post);
    }}