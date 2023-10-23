import {DateRange} from "@mui/lab";
import {DateTimeRange} from "../../../global/models/common";
import {Checkbox, IconButton, List, ListItem, ListItemButton, ListItemIcon, ListItemText} from "@mui/material";
import {useState} from "react";
import {format, parseISO} from "date-fns";
import pl from 'date-fns/locale/pl';


interface Props {
    dates: DateTimeRange[]
    onChange: (newDate: DateTimeRange) => void
}

export function DeliveryLocationCheckboxList({dates, onChange}: Props) {
    const [checked, setChecked] = useState<DateTimeRange | null>(null);

    const handleToggle = (date: DateTimeRange) => () => {
        setChecked(date);
        onChange(date)
    };

    return (
        <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
            {dates.map((date, i) => {
                const labelId = `checkbox-list-label-${date}`;

                return (
                    <ListItem
                        key={i}
                        disablePadding
                    >
                        <ListItemButton role={undefined} onClick={handleToggle(date)} dense>
                            <ListItemIcon>
                                <Checkbox
                                    edge="start"
                                    checked={checked === date}
                                    tabIndex={-1}
                                    disableRipple
                                    inputProps={{ 'aria-labelledby': labelId }}
                                />
                            </ListItemIcon>
                            <ListItemText id={labelId} primary={
                                `${format(new Date(date.start), 'dd.MM.yyyy HH:mm', { locale: pl })} - ${format(new Date(date.end), 'dd.MM.yyyy HH:mm', {locale: pl})}`} />
                        </ListItemButton>
                    </ListItem>
                );
            })}
        </List>
    );
}