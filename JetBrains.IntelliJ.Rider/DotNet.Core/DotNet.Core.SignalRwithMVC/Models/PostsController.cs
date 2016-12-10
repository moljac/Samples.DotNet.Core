namespace DotNet.Core.SignalR.Models
{
    public partial class PostsController
    {
        private IPostRepository _postRepository
        {
            get;
            set;
        }

        private Microsoft.AspNetCore.SignalR.Infrastructure.IConnectionManager _connectionManager
        {
            get;
            set;
        }

        public PostsController
                    (
                        IPostRepository postRepository,
                        Microsoft.AspNetCore.SignalR.Infrastructure.IConnectionManager connectionManager
                    )
        {
            _postRepository = postRepository;
            _connectionManager = connectionManager;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public System.Collections.Generic.List<Post> GetPosts()
        {
            return _postRepository.GetAll();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Post GetPost(int id)
        {
            return _postRepository.GetPost(id);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void AddPost(Post post)
        {
            _postRepository.AddPost(post);

            // SignalR
            _connectionManager.GetHubContext<PostsHub>().Clients.All.publishPost(post);

            return;
        }

    }
}