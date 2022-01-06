using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Data;

namespace WEB.Models
{
    public class ReservationCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;

        public void PopulateAssignedCategoryData(WEBContext context, Reservation reservation)
        {
            var allCategories = context.Category;
            var reservationCategories = new HashSet<int>(
                reservation.ReservationCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = reservationCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateReservationCategories(WEBContext context, string[] selectedCategories, Reservation reservationToUpdate)
        {
            if(selectedCategories==null)
            {
                reservationToUpdate.ReservationCategories = new List<ReservationCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var reservationCategories = new HashSet<int>
                (reservationToUpdate.ReservationCategories.Select(c => c.CategoryID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!reservationCategories.Contains(cat.ID))
                    {
                        reservationToUpdate.ReservationCategories.Add(
                            new ReservationCategory
                            {
                                ReservationID = reservationToUpdate.ID,
                                CategoryID = cat.ID
                            });
                    }
                }
                else
                {
                    if (reservationCategories.Contains(cat.ID))
                    {
                        ReservationCategory courseToRemove
                            = reservationToUpdate.ReservationCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}