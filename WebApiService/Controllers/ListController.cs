using WebApiService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizCommon_Std.Models;
using BizCommon_Std.FileIO;
using System.Text;
using Newtonsoft.Json;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 연결 목록을 가져오거나 서버 파일로 저장합니다.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListContext _context;

        public ListController(ListContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 저장되어 있는 연결 목록
        /// </summary>
        /// <returns></returns>
        // GET: api/List
        [HttpGet]
        public IEnumerable<ConnectionModel> GetConnectionList()
        {
            if (_context.ConnectionList.Count() == 0)
            {
                var conList = ReadFromFile();
                foreach (var connection in conList)
                {
                    _context.ConnectionList.Add(connection);
                }
            }
            _context.SaveChanges();

            return _context.ConnectionList;
        }

        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnectionList([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conItem = await _context.ConnectionList.FindAsync(id);

            if (conItem == null)
            {
                return NotFound();
            }

            return Ok(conItem);
        }

        // PUT: api/List/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConnectionList([FromRoute] long id, [FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(conItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectionItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/List
        [HttpPost]
        public async Task<IActionResult> PostConnectionList([FromBody] ConnectionModel conItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (conItem != null)
                conItem.Password = null;

            // Key : Title 
            if (_context.ConnectionList.Any(a => a.Title == conItem.Title))
            {
                return BadRequest("이미 있음");
            }
            _context.ConnectionList.Add(conItem);
            await _context.SaveChangesAsync();

            SaveToFile();

            return CreatedAtAction(nameof(GetConnectionList), new { id = conItem.Id }, conItem);
        }

        // DELETE: api/List/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnectionList([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conItem = await _context.ConnectionList.FindAsync(id);
            if (conItem == null)
            {
                return NotFound();
            }

            _context.ConnectionList.Remove(conItem);
            await _context.SaveChangesAsync();

            return Ok(conItem);
        }

        private bool ConnectionItemExists(long id)
        {
            return _context.ConnectionList.Any(e => e.Id == id);
        }

        /// <summary>
        /// 연결 목록 파일로 저장
        /// </summary>
        public void SaveToFile()
        {
            var saveList = _context.ConnectionList.Select(s => new ConnectionModel
            {
                DataSource = s.DataSource,
                InitialCatalog = s.InitialCatalog,
                Id = s.Id,
                Title = s.Title,
                UserID = s.UserID
            })
            .ToList();

            FileManager.FileReadWriter.FileWriter(FileManager.fileDir + @"\conList.txt", JsonConvert.SerializeObject(saveList));
        }

        public List<ConnectionModel> ReadFromFile()
        {
            var readData = FileManager.FileReadWriter.FileReaderToList(FileManager.fileDir + @"\conList.txt");
            if (readData == null || readData.Count == 0)
            {
                return new List<ConnectionModel>();
            }
            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < readData.Count; i++)// (var data in readData)
            //{
            //    if(i != readData.Count - 1)
            //    {
            //        sb.Append(readData[i] + ",");
            //    }
            //    sb.Append(data);
            //}
            List<ConnectionModel> retList = new List<ConnectionModel>();
            //foreach(var model in readData)
            //{
            //    retList.Add(JsonConvert.DeserializeObject<List<ConnectionModel>>(model));
            //}
            //return JsonConvert.DeserializeObject<List<ConnectionModel>>(sb.ToString());
            return JsonConvert.DeserializeObject<List<ConnectionModel>>(readData[0]);
        }
    }
}
