package tn.enit.crm.TicketingService.model;

import jakarta.persistence.DiscriminatorValue;
import jakarta.persistence.Entity;
import lombok.Data;
import lombok.EqualsAndHashCode;

@Entity
@DiscriminatorValue("Order")
@Data
@EqualsAndHashCode(callSuper = true)
public class OrderTicket extends Ticket {

    private Long orderId;

}

