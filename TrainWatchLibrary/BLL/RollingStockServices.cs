﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrainWatchSystem.DAL;
using TrainWatchSystem.Entities;

namespace TrainWatchSystem.BLL
{

    public class RollingStockServices
    {

        private readonly TrainWatchContext _context;


        internal RollingStockServices(TrainWatchContext regContext)
        {
            _context = regContext;
        }
        #region Services: Query


        public List <RollingStock> RollingStocks()

        { 
            IEnumerable <RollingStock> info = _context.RollingStocks.OrderBy(x => x.ReportingMark);
            return info.ToList();

        }

        public List<RailCarType> RailCarTypeList()

        {
           return _context.RailCarTypes.OrderBy(x => x.Name).ToList();
           

        }

        public List <RollingStock> GetByPartial(string SearchArg, int pageNumber,
                                                        int pageSize,
                                                        out int totalCount)
        {
            IEnumerable<RollingStock> info = _context.RollingStocks
                .Where(x => x.ReportingMark.Contains(SearchArg))
                .OrderBy(x => x.ReportingMark);

            totalCount = info.Count();
        
            int skipRows = (pageNumber - 1) * pageSize;

            return info.Skip(skipRows).Take(pageSize).ToList();

          
        }

        public List<RollingStock> GetByRailCarTypeID(int searcharg, int pageNumber,
                                                       int pageSize,
                                                       out int totalCount)
        {
            IEnumerable<RollingStock> info = _context.RollingStocks
                .Where(x => x.RailCarTypeID == searcharg)
                .OrderBy(x => x.ReportingMark);

            totalCount = info.Count();

            int skipRows = (pageNumber - 1) * pageSize;

            return info.Skip(skipRows).Take(pageSize).ToList();


        }

        public RollingStock GetByID(string searcharg)
        {
            RollingStock info = _context.RollingStocks
                                .Where(x => x.ReportingMark.Equals(searcharg))
                                .FirstOrDefault();
            return info;
        }

        public string RollingStock_AddStock(RollingStock item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Rolling Stock data is missing");
            }
            //this is an optional sample of validation of incoming data
            RollingStock exists = _context.RollingStocks
                            .Where(x => x.ReportingMark.Equals(item.ReportingMark) &&
                                        x.Owner.Equals(item.Owner) &&
                                        x.Capacity.Equals(item.Capacity) &&
                                        x.InService == item.InService)
                            .FirstOrDefault();
            if (exists != null)
            {
                throw new Exception($"{item.ReportingMark}");
            }

            //stage the data in local memory to be submitted to the database for 
            //  storage
            // a) what DbSet  
            // b) the action
            // c) indicate the data involved.

            //IMPORTANT: the data is in LOCAL memory; it has NOT!!!!!!! yet been sent
            //          to the database.
            //          THEREFORE: at this time, there is NO!!!!! primary key value (except
            //              for the system default (numerics -> 0)
            _context.RollingStocks.Add(item);

            // commit the LOCAL data to the database

            //IF there are validation annotations on your Entity
            //  they will be executed during the SaveChanges
            _context.SaveChanges();

            //AFTER the commit, your pkey value will NOW be available to you
            return item.ReportingMark;
        }

        //update
        //update can also have design considerations for validation
        //update may request that you check the record of interest is still
        //  on the database

        public string RollingStock_UpdateStock(RollingStock item)
        {
            //for an update, you MUST have the pkey value on your instance
            //if not, it will not work.

            // ***** WARNING ****** 
            // can cause PROBLEMS when being used with EntityEntry<t> processing

            //this technique returns an instance (object)
            //Product exists = _context.Products
            //                    .Where(x => x.ProductID == item.ProductID)
            //                    .FirstOrDefault();
            //if (exists == null)
            //{
            //    throw new Exception($"{item.ProductName} with a size of {item.QuantityPerUnit}" +
            //    $" from the selected supplier is not on file");
            //}

            // ****** BETTER ************
            // this does NOT actually return an instance and thus has no
            //   CONFLICT with using EntityEntry<T>

            //this technique does the search BUT returns only a boolean of success
            bool exists = _context.RollingStocks.Any(x => x.ReportingMark == item.ReportingMark);
            //if(!_context.Products.Any(x => x.ProductID == item.ProductID))
            if (!exists)
            {
                throw new Exception($"");
            }

            //stage the update
            EntityEntry<RollingStock> updating = _context.Entry(item);
            //flag the entity to be modified
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;


            //during the commit, SaveChanges() returns the number of rows affected
            _context.SaveChanges();
            return item.ReportingMark;
        }

        public string RollingStock_DeleteStock(RollingStock item)
        {
            //for an delete, you MUST have the pkey value on your instance
            //if not, it probably will not work as expected.

            //this technique returns an instance (object)
            //Product exists = _context.Products
            //                    .Where(x => x.ProductID == item.ProductID)
            //                    .FirstOrDefault();

            //this technique does the search BUT returns only a boolean of success
            bool exists = _context.RollingStocks.Any(x => x.ReportingMark == item.ReportingMark);

            //DEPENDING on which technique you use, your error test will be different
            //one:   if (exists == null) {....}
            if (!exists)
            {
                throw new Exception($"");
            }

            //Removing a record from your database maybe a
            // a) phyiscal act
            //   OR
            // b) logical act

            // a) if you wish the record to be "phyiscally remove from the databae
            //    you will use the staging of .Deleted
            //    if the record being removed from the database is a "parent" record
            //      (referenced in a foreign key), the delete WILL FAIL in a relational
            //      database IF there are existing "children" or the record
            //    sql "parent records cannot be deleted if children exist"
            //    the decission to automatically remove "children" is a system design decission 

            //stage the phyiscal delete
            //EntityEntry<Product> deleting = _context.Entry(item);
            //flag the entity to be deleted
            //.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            // b) Locial delete
            //      this is where you do not or cannot phyiscally remove a record from
            //      the database.
            //      This decission is based on existing best practice business rules OR
            //          set by government regulations
            //      this type of delete is done so the "flagged" data is NOT used in
            //          daily processing
            //
            //   this type of delete will actually be an update the attribute (property) 
            //      of the record.
            //   Look for attributes such as Active, Discontinued, a special date ReleaseDate

            // Product is a logical delete (Discontinued = true;)

            //stage the logical delete
            item.InService = true;
            EntityEntry<RollingStock> updating = _context.Entry(item);
            //flag the entity to be deleted
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;



            //during the commit, SaveChanges() returns the number of rows affected
            _context.SaveChanges();
            return item.ReportingMark;
        }

    }
}
#endregion