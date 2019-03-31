using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    /// <summary>
    /// This class of StudentResult contains specific Attributes according to the database StudentResult table
    /// </summary>
    class StudentResult
    {
        private int studentId;
        private int assessmentComponentId;
        private int rubricMeasurementId;
        private DateTime evaluationDate;

        public int StudentId { get => studentId; set => studentId = value; }
        public int AssessmentComponentId { get => assessmentComponentId; set => assessmentComponentId = value; }
        public int RubricMeasurementId { get => rubricMeasurementId; set => rubricMeasurementId = value; }
        public DateTime EvaluationDate { get => evaluationDate; set => evaluationDate = value; }
    }
}
