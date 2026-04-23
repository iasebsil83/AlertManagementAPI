using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlertManagementAPI;
using AlertManagementAPI.Models;

namespace AlertManagementAPI.Controllers {



    //alerts
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase {



        //attributes
        private readonly AlertContext _context;



        //init
        public AlertController(AlertContext context) {
            _context = context; //bind context for internal use
        }



        // GET: api/Alert
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlerts() {
            return await _context.Alerts.ToListAsync(); //every alert
        }



        // GET: api/Alert/#ID#
        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(int id) {

            //get by ID
            var alert = await _context.Alerts.FindAsync(id);

            //not found
            if (alert == null) { return NotFound(); }
            return alert;
        }



        // PUT: api/Alert/#ID#
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{oldId}")]
        public async Task<IActionResult> PutAlert(int oldID, Alert newAlert) {

            //get original target
            var oldAlert = await _context.Alerts.FindAsync(oldID);
            if(oldAlert == null){ return NotFound(); }

            //immutable values
            newAlert.ID        = oldID;
            newAlert.CreatedAt = oldAlert.CreatedAt;

            //incorrect modification
            if(
                (
                    (newAlert.Message != oldAlert.Message || newAlert.Area != oldAlert.Area) && //different content (message/area)
                    oldAlert.Status != STATUS.DRAFT                                             //forbidden case: if !DRAFT
                ) || (
                    !Enum.IsDefined(typeof(STATUS), newAlert.Status) || //new status out of range

                    (oldAlert.Status == STATUS.DRAFT     && newAlert.Status == STATUS.CANCELLED) || //forbidden case: DRAFT     => CANCELLED
                    (oldAlert.Status == STATUS.PUBLISHED && newAlert.Status == STATUS.DRAFT    ) || //forbidden case: PUBLISHED => DRAFT
                    (oldAlert.Status == STATUS.CANCELLED && newAlert.Status != STATUS.CANCELLED)    //forbidden case: CANCELLED => !CANCELLED
                )
            ){ return BadRequest(); }

            //set marker "has been modified at least once"
            _context.Entry(newAlert).State = EntityState.Modified;

            //try saving to database
            try{
                await _context.SaveChangesAsync();

            //modifications occured in DB in the meantime => our request can be outdated
            }catch (DbUpdateConcurrencyException){
                if (!AlertExists(oldID)){
                    return NotFound(); //ID was not in use yet => new item to be PUT => no sync problems then
                }else{
                    throw; //otherwise => report potential problem to user!
                }
            }
            return NoContent();
        }



        // POST: api/Alert
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alert>> PostAlert(Alert alert) {

            //get latest ID available
            int lastID = _context.Alerts.Count();

			//set initial attributes
            alert.ID        = lastID + 1;
            alert.Status    = STATUS.DRAFT;
            alert.CreatedAt = DateOnly.FromDateTime(DateTime.Now);

            //add a new alert
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            //feedback
            return CreatedAtAction("GetAlert", new { id = alert.ID }, alert);
        }



        // DELETE: api/Alert/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id) {
            var alert = await _context.Alerts.FindAsync(id);

            //not found
            if (alert == null) { return NotFound(); }

            //delete
            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        //tools
        private bool AlertExists(int id) {
            return _context.Alerts.Any(e => e.ID == id);
        }
    }
}
