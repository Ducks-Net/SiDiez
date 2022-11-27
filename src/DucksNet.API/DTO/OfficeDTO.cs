﻿namespace DucksNet.API.DTO;

public class OfficeDTO
{
    public Guid BusinessId { get; set; }
    public string Address {get; set;} = string.Empty;
    public int AnimalCapacity {get; set;} = -1;
} 
