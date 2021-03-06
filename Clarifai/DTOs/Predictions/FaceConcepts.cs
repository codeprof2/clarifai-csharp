﻿using System.Collections.Generic;

namespace Clarifai.DTOs.Predictions
{
    public class FaceConcepts : IPrediction
    {
        public string TYPE => "faceConcepts";

        public Crop Crop { get; }

        public List<Concept> Concepts { get; }

        private FaceConcepts(Crop crop, List<Concept> concepts)
        {
            Crop = crop;
            Concepts = concepts;
        }

        public static FaceConcepts Deserialize(dynamic jsonObject)
        {
            var concepts = new List<Concept>();
            foreach (dynamic concept in jsonObject.data.face.identity.concepts)
            {
                concepts.Add(Concept.Deserialize(concept));
            }
            return new FaceConcepts(DTOs.Crop.Deserialize(jsonObject.region_info.bounding_box), concepts);
        }

        public override bool Equals(object obj)
        {
            return obj is FaceConcepts concepts &&
                   EqualityComparer<Crop>.Default.Equals(Crop, concepts.Crop) &&
                   EqualityComparer<List<Concept>>.Default.Equals(Concepts, concepts.Concepts);
        }

        public override int GetHashCode()
        {
            var hashCode = -1453176561;
            hashCode = hashCode * -1521134295 + EqualityComparer<Crop>.Default.GetHashCode(Crop);
            hashCode = hashCode * -1521134295 +
                       EqualityComparer<List<Concept>>.Default.GetHashCode(Concepts);
            return hashCode;
        }

        public override string ToString()
        {
            return $"[FaceConcepts: (crop: {Crop}, concepts: {Concepts})]";
        }
    }
}
