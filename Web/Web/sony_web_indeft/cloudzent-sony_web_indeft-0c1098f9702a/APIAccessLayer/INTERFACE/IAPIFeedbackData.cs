using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIFeedbackData
    {
        Envelope<bool> CreateFeedbackForm(CreateFeedbackForm objFormData);
        Envelope<bool> DeleteFeedbackForm(DeleteFeedbackForm objFormData);
        Envelope<CreateFeedbackForm> ViewFeedbackFormDesign(ViewFeedbackForm objFormData);

        Envelope<bool> CreateTrainingForm(CreateTrainingForm objFormData);

        Envelope<bool> InActiveTrainingMessage(CreateTrainingForm objFormData);
    }
}
