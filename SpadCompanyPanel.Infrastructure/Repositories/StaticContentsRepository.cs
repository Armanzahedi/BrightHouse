//using SpadCompanyPanel.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SpadCompanyPanel.Infrastructure.Repositories
//{
//   public class StaticContentsRepository : BaseRepository<StaticContentType, MyDbContext>
//    {
//        private readonly MyDbContext _context;
//        public StaticContentsRepository(MyDbContext context) : base(context)
//        {
//            _context = context;
//        }
//        public async Task<StaticContentType> Get(string identifier)
//        {
//            var contentType = await _context.StaticContentTypes.FirstOrDefaultAsync(c => c.Identifier.ToLower().Equals(identifier.ToLower()));
//            return contentType;
//        }
//        public async Task<StaticContentType> DeleteContentType(int id)
//        {
//            var contentType = await _context.StaticContentTypes.Include(a => a.StaticContentDetails).FirstOrDefaultAsync(a => a.Id == id);
//            if (contentType == null)
//                return contentType;

//            _context.StaticContentDetails.RemoveRange(contentType.StaticContentDetails);
//            _context.StaticContentTypes.Remove(contentType);
//            await _context.SaveChangesAsync();
//            return contentType;
//        }
//        public async Task<List<StaticContentDetail>> GetContentDetailsList(int contentTypeId)
//        {
//            var contentDetails = await _context.StaticContentDetails.Where(c=>c.StaticContentTypeId == contentTypeId).ToListAsync();
//            return contentDetails;
//        }
//        public async Task<List<StaticContentDetail>> GetContentDetailsList(string identifier)
//        {
//            var contentDetails = await _context.StaticContentDetails.Where(c => c.StaticContentType.Identifier.ToLower().Equals(identifier.ToLower())).ToListAsync();
//            return contentDetails;
//        }
//        public async Task<StaticContentDetail> GetContentDetail(int? id)
//        {
//            var contentDetail = await _context.StaticContentDetails.FindAsync(id);
//            if (contentDetail == null)
//                return null;
//            return contentDetail;
//        }
//        public async Task<StaticContentDetail> GetContentDetail(int typeId, string identifier)
//        {
//            var contentDetail = await _context.StaticContentDetails.FirstOrDefaultAsync(c=> c.StaticContentTypeId == typeId && c.Identifier.ToLower().Equals(identifier.ToLower()));
//            if (contentDetail == null)
//                return null;
//            return contentDetail;
//        }
//        public async Task<StaticContentDetail> GetContentDetail(string typeIdentifier, string identifier)
//        {
//            var contentDetail = await _context.StaticContentDetails.Include(c=>c.StaticContentType).FirstOrDefaultAsync(c => c.StaticContentType.Identifier.ToLower().Equals(typeIdentifier.ToLower()) && c.Identifier.ToLower().Equals(identifier.ToLower()));
//            if (contentDetail == null)
//                return null;
//            return contentDetail;
//        }
//        public async Task<StaticContentDetail> DeleteContentDetail(int typeId,int id)
//        {
//            var contentDetail = await _context.StaticContentDetails.FirstOrDefaultAsync(c=>c.StaticContentTypeId == typeId && c.Id == id);
//            if (contentDetail == null)
//                return null;
//            _context.StaticContentDetails.Remove(contentDetail);
//            await _context.SaveChangesAsync();
//            return contentDetail;
//        }
//        public async Task<StaticContentDetail> AddContentDetail(StaticContentDetail model)
//        {
//            _context.StaticContentDetails.Add(model);
//            await _context.SaveChangesAsync();
//            return model;
//        }
//        public async Task<StaticContentDetail> UpdateContentDetail(StaticContentDetail model)
//        {
//            var contentDetail = await _context.StaticContentDetails.FindAsync(model.Id);
//            if (contentDetail == null)
//                return null;

//            _context.Entry(contentDetail).State = EntityState.Detached;
//            _context.Entry(model).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//            return model;
//        }
//        //public async Task<StaticContentDetail> UploadContentImage(int id, IFormFile file)
//        //{
//        //    var contentDetail = await _context.StaticContentDetails.FindAsync(id);

//        //    if (contentDetail.Image != null)
//        //        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", contentDetail.Image)))
//        //            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", contentDetail.Image));

//        //    var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
//        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", imageName);
//        //    using var stream = new FileStream(filePath, FileMode.Create);
//        //    file.CopyTo(stream);

//        //    contentDetail.Image = imageName;
//        //    _context.StaticContentDetails.Update(contentDetail);
//        //    await _context.SaveChangesAsync();

//        //    return contentDetail;
//        //}
//    }
//}
