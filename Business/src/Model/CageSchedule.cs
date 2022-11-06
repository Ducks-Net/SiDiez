namespace VetAppointment.Model;

class CageSchedule {
    public Guid ID { get; }
    public Size size { get; }
    public List<Cage> cages { get; }
    public List<Tuple<Cage, DateTime, DateTime>> schedule { get; } // NOTE(Al-Andrew): tuple description: usedCage, timeStart, timeEnd
}