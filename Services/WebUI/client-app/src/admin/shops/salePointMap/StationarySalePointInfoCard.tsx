import {StationaryShop} from "../../../global/models/shop";
import {
    Divider,
    List,
    ListItem, Paper,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    Typography
} from "@mui/material";
import {SellersTable} from "./SellersTable";
import {OpenHoursTable} from "./OpenHoursTable";

interface Props {
    shop: StationaryShop
}

export function StationarySalePointInfoCard({shop}: Props) {
    return (
        <Stack direction={'column'} spacing={3} style={{height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <Typography style={{height: '10%'}} variant={'h4'}>{shop.shopName}</Typography>
            <Divider style={{width: '100%'}} />
            <Paper style={{padding: '20px', minWidth: '100%', overflow: 'scroll', boxShadow: 'none', border: '1px solid #BBBBBB', borderRadius: '10px'}}>
                <SellersTable sellers={shop.sellers}/>
                <OpenHoursTable workingDays={shop.weekSchedule} />
            </Paper>
        </Stack>
    );
}