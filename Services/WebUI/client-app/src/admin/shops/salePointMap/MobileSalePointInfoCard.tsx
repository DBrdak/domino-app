import {MobileShop, SalePoint} from "../../../global/models/shop";
import {Location} from '../../../global/models/common'
import {Divider, Paper, Stack, Typography} from "@mui/material";
import {SellersTable} from "./SellersTable";
import {OpenHoursTable} from "./OpenHoursTable";
import {useEffect} from "react";

interface Props {
    shops: MobileShop[]
    location: Location
}

export function MobileSalePointInfoCard({shops, location}: Props) {

    return (
        <Stack direction={'column'} spacing={3} style={{height: '100%', overflow: 'auto', paddingRight: '5px'}}>
            {shops.map(ms => (
                <Stack key={ms.id} direction={'column'} spacing={3}
                       style={{height: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                    <Typography style={{height: '10%'}} variant={'h4'}>
                        {ms.shopName}
                    </Typography>
                    <Divider style={{width: '100%'}}/>
                    <Paper style={{
                        padding: '20px',
                        minWidth: '100%',
                        overflow: 'auto',
                        boxShadow: 'none',
                        border: '1px solid #BBBBBB',
                        borderRadius: '10px'
                    }}>
                        <SellersTable key={ms.id} sellers={ms.sellers}/>
                        <OpenHoursTable
                            workingDays={ms.salePoints
                                .filter(sp => sp.location.name === location.name)
                                .map(spl => ({
                                    openHours: spl.openHours,
                                    cachedOpenHours: spl.openHours,
                                    isClosed: spl.isClosed,
                                    weekDay: spl.weekDay
                                }))
                            }
                        />
                    </Paper>
                </Stack>
            ))}
        </Stack>
    );
}