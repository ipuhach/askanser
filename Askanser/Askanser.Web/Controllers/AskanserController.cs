using Askanser.Core.Interfaces;
using Askanser.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Askanser.Web.Controllers
{
    [Authorize]
    public class AskanserController : Controller
    {
        IAskanserProvider askanserManager;

        public AskanserController(IAskanserProvider askanserProvider)
        {
            askanserManager = askanserProvider;
        }

        public ActionResult Index()
        {
            //ViewBag.Questions = new List<Question>(new Question[] { new Question { Id = 0, Text = "Que1" }, new Question { Id = 1, Text = "Que2" }, new Question { Id = 2, Text = "Que3" } });
            return RedirectToAction("Questions");
        }

        [AllowAnonymous]
        public ActionResult TopOneQuanswer()
        {
            ViewBag.TopQuanswer = askanserManager.GetTopOneQuanswer();
            return PartialView();
        }


        public ActionResult Questions()
        {
            //ViewBag.Questions = new List<Question>(new Question[] { new Question { Id = 0, Text = "Que1" }, new Question { Id = 1, Text = "Que2" }, new Question { Id = 2, Text = "Que3" } });
            ViewBag.Questions = askanserManager.GetQuestions();
            return View();
        }



        public ActionResult Answer(int id)
        {
            AnswerViewModel model = new AnswerViewModel()
            {
                Question = askanserManager.GetQuestion(id)
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Answer(AnswerViewModel RedeemModel)
        {
            if (ModelState.IsValid)
            {
                //insert db call here
                // нужно сделать коммон с юзерайди для квесшена, и еще как то что бы ID сюда пришло tempdata вроде должен работать, но вопрос с юзерайди вопроса остаетс яоткрытым
                // кстати еще тебе нужно сделать оценку чужих ответов, и просмотр своих ответов(???)


                bool flag = askanserManager.AddAnswer(RedeemModel.Question.Id, RedeemModel.Text, this.User.Identity.GetUserId());
                if (flag) return Redirect("/");

            }

            return View(RedeemModel);
        }



        public ActionResult Ask()
        {
            //ViewBag.Questions = new List<Question>(new Question[] { new Question { Id = 0, Text = "Que1" }, new Question { Id = 1, Text = "Que2" }, new Question { Id = 2, Text = "Que3" } });
            return View();
        }

        public ActionResult AnswersToMyQuestions()
        {
            ViewBag.Quanswers = askanserManager.GetAnswersForMyQuestions(this.User.Identity.GetUserId());

            return View();
        }


        public ActionResult MyAnswers()
        {
            ViewBag.Quanswers = askanserManager.GetMyAnswers(this.User.Identity.GetUserId());
            return View();
        }

        public ActionResult SetScore(int id)
        {

            QuanswerViewModel model = new QuanswerViewModel()
            {
                Quanswer = askanserManager.GetAnswerForMyQuestion(id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetScore(QuanswerViewModel RedeemModel)
        {
            if (ModelState.IsValid)
            {


                bool flag = askanserManager.SetScore(RedeemModel.Quanswer.AnsId, RedeemModel.Score);
                if (flag) return Redirect("/");

            }

            return View(RedeemModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ask(AskViewModel model)
        {
            if (ModelState.IsValid)
            {
                //insert db call here
                bool flag = askanserManager.AddQuestion(model.Text, this.User.Identity.GetUserId());
                if (flag) return Redirect("/");
            }

            return View(model);
        }
    }
}