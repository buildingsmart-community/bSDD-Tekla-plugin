
using BsddTeklaPlugin.Helpers;
using System.Collections.Generic;
using System.Linq;
using Tekla.BIM.Geometry;
using Tekla.Common.Geometry;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using static Tekla.Structures.Plugins.PluginBase;

namespace BsddTeklaPlugin.Plugin
{
    internal class PluginInitiator
    {


        public bool Run(List<InputDefinition> Input)
        {
            List<Part> modelParts = Input.GetMultipleModelparts(1);
            if (modelParts.Count == 0)
            {
                return false;
            }
            Beam beam = modelParts[0] as Beam;


            Solid part1solid = beam.GetSolid(Solid.SolidCreationTypeEnum.PLANECUTTED);

            List<Face3> part1solidfaces = part1solid.GetFaces().ToList();

            UnitVector3 direction = (beam.EndPoint - beam.StartPoint).ToVector3().GetNormalized();
            UnitVector3 perpendiculardirection = new UnitVector3(direction.Y, -direction.X, direction.Z);

            Segment3 segment = new Segment3(beam.StartPoint.ToVector3(), beam.EndPoint.ToVector3());

            Face3 closesFace = part1solidfaces.Where(f => f.Normal.ToPoint() == perpendiculardirection.ToPoint()).Aggregate((p1, p2) => p1.Points.First().DistanceTo(beam.StartPoint.ToVector3()) < p2.Points.First().DistanceTo(beam.StartPoint.ToVector3()) ? p1 : p2);

            double distance = System.Math.Round(closesFace.DistanceTo(beam.StartPoint.ToVector3()), 2);

            Segment3 frontsegment = new Segment3(segment.NegativeEnd + (distance * perpendiculardirection), segment.PositiveEnd + (distance * perpendiculardirection));

         
            return true;
        }
    }
}
