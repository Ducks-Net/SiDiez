namespace VetAppointment.Model;

class CageSchedule {
    public Guid ID { get; }
    public Size Size { get; }
    public List<Cage> Cages { get; }
    public List<Tuple<Cage, DateTime, DateTime>> Schedule { get; } // NOTE(Al-Andrew): tuple description: usedCage, timeStart, timeEnd
}