import {Box, Button} from "@mui/material";
import PriceListCreateModal from "../../priceListCreation/PriceListCreateModal";
import {AddCircleOutlined} from "@mui/icons-material";
import React from "react";
import {observer} from "mobx-react-lite";
import LineItemCreateModal from "./LineItemCreateModal";
import {useStore} from "../../../../global/stores/store";

interface Props {
    priceListId: string
}

function LineItemCreateSection({priceListId}: Props) {
    const {modalStore} = useStore()

    return (
        <Box display={'flex'} justifyContent={'right'}>
            <Button color='secondary' style={{display: 'flex', gap: '10px', backgroundColor: '#C32B28',
                justifyContent: 'center', alignItems: 'center'}} onClick={() => modalStore.openModal(<LineItemCreateModal priceListId={priceListId} />)}>
                <AddCircleOutlined fontSize='medium' />
                Dodaj nowy produkt
            </Button>
        </Box>
    );
}

export default observer(LineItemCreateSection)