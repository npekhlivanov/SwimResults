namespace SwimResults.Pages
{
    using AutoMapper;
    using Constants;
    using DataAccess.Data;
    using DataAccess.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SwimResults.Models;
    using System.Threading.Tasks;

    public class EditIntervalModel : PageModel
    {
        private readonly WorkoutIntervalRepository _intervalRepository;
        private readonly WorkoutIntervalTypeRepository _intervalTypeRepository;
        private readonly IMapper _mapper;

        public EditIntervalModel(WorkoutIntervalRepository intervalRepository, WorkoutIntervalTypeRepository intervalTypeRepository, IMapper mapper)
        {
            _intervalRepository = intervalRepository;
            _intervalTypeRepository = intervalTypeRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public WorkoutIntervalEditModel WorkoutInterval { get; set; }

        public SelectList WorkoutIntervalTypeSelectList { get; private set; }

        //[TempData]
        //public string ReturnPath { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string returnPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedInterval = await _intervalRepository.Get(id.Value, w => w.WorkoutIntervalType);
            if (storedInterval == null)
            {
                return NotFound();
            }

            WorkoutInterval = _mapper.Map<WorkoutIntervalEditModel>(storedInterval);
            var returnUrl = string.IsNullOrWhiteSpace(returnPath) ? Url.Page("Details", new { id = storedInterval.WorkoutId }): returnPath; // Request.Headers["Referer"]
            TempData[ValueKeys.TempDataReturnPathKey] = returnUrl;
            //ReturnPath = returnPath; // works fine without extra configuring; get value in page via @Model.ReturnPath 

            var intervalTypes = await _intervalTypeRepository.GetList();
            WorkoutIntervalTypeSelectList = new SelectList(intervalTypes, "Id", "Name", storedInterval.WorkoutIntervalTypeId ?? 0);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var storedInterval = await _intervalRepository.Get(WorkoutInterval.Id);
            var modifiedInterval = _mapper.Map<WorkoutInterval>(storedInterval);
            _mapper.Map(WorkoutInterval, modifiedInterval);

            try
            {
                await _intervalRepository.UpdateModifiedFields(modifiedInterval, storedInterval);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutIntervalExists(WorkoutInterval.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //var returnUrl = ReturnPath;
            //ReturnPath = null;
            var returnUrl = (string)TempData[ValueKeys.TempDataReturnPathKey];
            TempData.Clear();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("./IntervalDetails", new { id = WorkoutInterval.Id });
        }

        private bool WorkoutIntervalExists(int id)
        {
            return _intervalRepository.Get(id) != null;
        }
    }
}
