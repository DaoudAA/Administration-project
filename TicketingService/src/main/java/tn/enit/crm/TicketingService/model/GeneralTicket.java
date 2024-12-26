package tn.enit.crm.TicketingService.model;

import jakarta.persistence.DiscriminatorValue;
import jakarta.persistence.Entity;
import lombok.Data;
import lombok.EqualsAndHashCode;

@Entity
@DiscriminatorValue("General")
@Data
@EqualsAndHashCode(callSuper = true)
public class GeneralTicket extends Ticket {
}
