using socialNetworkApi.Models;
using socialNetworkApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace socialNetworkApi.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostController : ApiController
    {

        IPostRepository pRepo = new PostRepository();
        // GET api/<controller>
        [Route("")]
        public IHttpActionResult Get()
        {
            var posts = pRepo.GetAll().ToList();
            foreach(Post post in posts)
            {
                post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "GET", Rel = "Read all posts" });
                post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "GET", Rel = "Read a specific post" });
                post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "POST", Rel = "Create a new post" });
                post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "PUT", Rel = "Update specified post" });
                post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "DELETE", Rel = "Delete specified post" });

            }
            return Ok(posts);
        }

        // GET api/<controller>/5
        [Route("{id}", Name = "GetPostByID")]
        public IHttpActionResult Get(int id)
        {
            Post post = pRepo.Get(id);
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "GET", Rel = "Read all posts" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "GET", Rel = "Read a specific post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "POST", Rel = "Create a new post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "PUT", Rel = "Update specified post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "DELETE", Rel = "Delete specified post" });
            return Ok(post);
        }

        // POST api/<controller>
        [Route("")]
        public IHttpActionResult Post([FromBody]Post post)
        {
            pRepo.Insert(post);
            string url = Url.Link("GetPostByID", new { id = post.id });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "GET", Rel = "Read all posts" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "GET", Rel = "Read a specific post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "POST", Rel = "Create a new post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "PUT", Rel = "Update specified post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "DELETE", Rel = "Delete specified post" });

            return Created(url, post);
        }

        // PUT api/<controller>/5
        [Route("{id}")]
        public IHttpActionResult Put([FromBody]Post post, [FromUri]int id)
        {
            post.id = id;
            pRepo.Update(post);
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "GET", Rel = "Read all posts" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "GET", Rel = "Read a specific post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts", Method = "POST", Rel = "Create a new post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "PUT", Rel = "Update specified post" });
            post.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + post.id, Method = "DELETE", Rel = "Delete specified post" });

            return Ok(post);
        }

        // DELETE api/<controller>/5
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            ICommentRepository cRepo = new CommentRepository();
            cRepo.DeleteAll(id);
            pRepo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}