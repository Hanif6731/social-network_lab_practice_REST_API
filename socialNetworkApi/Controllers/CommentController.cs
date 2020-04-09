using socialNetworkApi.Models;
using socialNetworkApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace socialNetworkApi.Controllers
{
    [RoutePrefix("api/posts/{pid}/comments")]
    public class CommentController : ApiController
    {
        ICommentRepository cRepo = new CommentRepository();
        // GET api/<controller>
        [Route("")]
        public IHttpActionResult Get(int pid)
        {
            var comments = cRepo.GetAll(pid).ToList();
            foreach (Comment comment in comments)
            {
                comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/"+comment.postId+"/comments", Method = "GET", Rel = "Read all comments for a post" });
                comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "GET", Rel = "Read one comment for a post" });
                comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts" + comment.postId + "/comments", Method = "POST", Rel = "Create a comment for a post" });
                comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "PUT", Rel = "Update specified comment" });
                comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posys/" + comment.postId + "/comments/" + comment.id, Method = "DELETE", Rel = "Delete specified comment" });

            }
            return Ok(comments);
        }

        // GET api/<controller>/5
        [Route("{id}", Name = "GetCommentByID")]
        public IHttpActionResult Get(int pid, int id)
        {
            Comment comment = cRepo.Get(pid, id);
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments", Method = "GET", Rel = "Read all comments for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "GET", Rel = "Read one comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts" + comment.postId + "/comments", Method = "POST", Rel = "Create a comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "PUT", Rel = "Update specified comment" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posys/" + comment.postId + "/comments/" + comment.id, Method = "DELETE", Rel = "Delete specified comment" });

            return Ok(comment);
        }

        // POST api/<controller>
        [Route("")]
        public IHttpActionResult Post([FromBody]Comment comment,[FromUri]int pid)
        {
            comment.postId = pid;
            cRepo.Insert(comment);
            string url = Url.Link("GetPostByID", new { id = comment.id });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments", Method = "GET", Rel = "Read all comments for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "GET", Rel = "Read one comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts" + comment.postId + "/comments", Method = "POST", Rel = "Create a comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "PUT", Rel = "Update specified comment" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posys/" + comment.postId + "/comments/" + comment.id, Method = "DELETE", Rel = "Delete specified comment" });

            return Created(url, comment);
        }

        // PUT api/<controller>/5
        [Route("{id}")]
        public IHttpActionResult Put([FromBody]Comment comment, [FromUri]int pid, [FromUri]int id)
        {
            comment.postId = pid;
            comment.id = id;
            cRepo.Update(comment);
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments", Method = "GET", Rel = "Read all comments for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "GET", Rel = "Read one comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts" + comment.postId + "/comments", Method = "POST", Rel = "Create a comment for a post" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posts/" + comment.postId + "/comments/" + comment.id, Method = "PUT", Rel = "Update specified comment" });
            comment.Links.Add(new Links() { HRef = "http://localhost:44367/api/posys/" + comment.postId + "/comments/" + comment.id, Method = "DELETE", Rel = "Delete specified comment" });

            return Ok(comment);
        }

        // DELETE api/<controller>/5
        [Route("{id}")]
        public IHttpActionResult Delete(int pid, int id)
        {
            cRepo.Delete(pid, id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}